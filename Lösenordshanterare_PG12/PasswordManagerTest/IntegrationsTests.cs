using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Lösenordshanterare_PG12;

namespace PasswordManagerTest
{
    [TestClass]
    public class ManagerTest
    {
        // Arbitrary input values used in many tests.
        string pwd1 = "1234567890";
        string pwd2 = "0987654321";
        string serverPath;
        string clientPath1;
        string clientPath2;

        // Used to mock Console and capture output.
        StringWriter output;


        ////
        /// Setup and TearDown
        ///


        [TestInitialize]
        public void Setup()
        {
            this.serverPath = ("server.json");
            this.clientPath1 = ("client1.json");
            this.clientPath2 = ("client2.json");
            resetConsoleOutput();
        }

        [TestCleanup]
        public void TearDown()
        {
            if (File.Exists(serverPath)) File.Delete(serverPath);
            if (File.Exists(clientPath1)) File.Delete(clientPath1);
            if (File.Exists(clientPath2)) File.Delete(clientPath2);
        }


        ////
        /// Tests
        ///


        [TestMethod]
        public void InitCommandCreatesClientAndServer()
        {
            init(clientPath1, serverPath, pwd1);
            Assert.IsTrue(File.Exists(clientPath1), "The command 'init' should create client file.");
            Assert.IsTrue(File.Exists(serverPath), "The command 'init' should create server file.");
        }

        [TestMethod]
        public void InitCommandOverwritesClientAndServer()
        {
            // Create
            init(clientPath1, serverPath, pwd1);
            string oldClient = File.ReadAllText(clientPath1).Trim();
            string oldServer = File.ReadAllText(serverPath).Trim();

            // Overwrite
            init(clientPath1, serverPath, pwd1);
            string newClient = File.ReadAllText(clientPath1).Trim();
            string newServer = File.ReadAllText(serverPath).Trim();

            // Assert inequality
            Assert.AreNotEqual(oldClient, newClient, "Running the command 'init' twice, should overwrite the client.");
            Assert.AreNotEqual(oldServer, newServer, "Running the command 'init' twice, should overwrite the server.");
        }

        [TestMethod]
        public void SecretKeyCanBeExtractedAndUsedToLogInToExistingVault()
        {
            init(clientPath1, serverPath, pwd1);
            string secret1 = extractSecretKey(clientPath1);
            login(clientPath2, serverPath, pwd1, secret1);
            string secret2 = extractSecretKey(clientPath2);
            Assert.AreEqual(secret1, secret2, "When the command 'login' is used to log in multiple clients to the same server, all clients should have the same secret.");
        }

        [TestMethod]
        public void CannotLoginUsingIncorrectSecretKey()
        {
            init(clientPath1, serverPath, pwd1);
            loginUsingRandomSecretKey(clientPath2, serverPath, pwd1);
            Assert.IsFalse(File.Exists(clientPath2), "The command 'login' should not create a client when the secret key is incorrect.");
        }

        [TestMethod]
        public void CannotLoginUsingIncorrectMasterPwd()
        {
            init(clientPath1, serverPath, "password");
            string secret1 = extractSecretKey(clientPath1);
            login(clientPath2, serverPath, "wrong", secret1);
            Assert.IsFalse(File.Exists(clientPath2), "The command 'login' should not create a client when the master password is incorrect.");
        }

        [TestMethod]
        public void GetCommandCanRetriveIndividuallySetData()
        {
            string prop1 = "some arbitrary prop 1";
            string prop2 = "some arbitrary prop 2";
            string data1 = "some arbitrary data 1";
            string data2 = "some arbitrary data 2";

            // Setup vault and store data.
            init(clientPath1, serverPath, pwd1);
            set(clientPath1, serverPath, pwd1, prop1, data1);
            set(clientPath1, serverPath, pwd1, prop2, data2);

            // Read raw storage and assert non-plain-text.
            string server = File.ReadAllText(serverPath);
            Assert.IsFalse(server.Contains(data1), "The command 'set' must not store data in plain-text.");
            Assert.IsFalse(server.Contains(data2), "The command 'set' must not store data in plain-text.");

            // Retrieve first key-value pair and assert match.
            string response1 = get(clientPath1, serverPath, pwd1, prop1);
            StringAssert.Contains(response1, data1, "The command 'get' must retrieve decrypted values.");

            // Retrieve second key-value pair and assert match.
            string response2 = get(clientPath1, serverPath, pwd1, prop2);
            StringAssert.Contains(response2, data2, "The command 'get' must retrieve decrypted values.");
        }

        [TestMethod]
        public void GetCommandCanRetrieveSetKeys()
        {
            string prop1 = "arbitrary prop 1";
            string prop2 = "arbitrary prop 2";

            // Setup vault and store data.
            init(clientPath1, serverPath, pwd1);
            set(clientPath1, serverPath, pwd1, prop1, "arbitrary1");
            set(clientPath1, serverPath, pwd1, prop2, "arbitrary2");

            // Retrieve output and assert keys are included.
            string keysResponse = getKeys(clientPath1, serverPath, pwd1);
            StringAssert.Contains(keysResponse, prop1, "The command 'get' must list all keys in plain-text.");
            StringAssert.Contains(keysResponse, prop2, "The command 'get' must list all keys in plain-text.");
        }

        [TestMethod]
        public void GetCommandDoesNotSeeDroppedValues()
        {
            string prop1 = "prop that will stay";
            string prop2 = "will be dropped";
            string data1 = "value that will stay";
            string data2 = "value that will be dropped";

            // Setup vault, store two pieces, and then drop second.
            init(clientPath1, serverPath, pwd1);
            set(clientPath1, serverPath, pwd1, prop1, data1);
            set(clientPath1, serverPath, pwd1, prop2, data2);
            drop(clientPath1, serverPath, pwd1, prop2);

            // Retrieve list of keys and assert output only contains first prop.
            string keysReponse = getKeys(clientPath1, serverPath, pwd1);
            StringAssert.Contains(keysReponse, prop1, "The command 'get' must exclude all dropped keys.");
            Assert.IsFalse(keysReponse.Contains(prop2), "The command 'get' must exclude all dropped keys.");

            // Retrieve first key-value pair and assert match.
            string prop1Response = get(clientPath1, serverPath, pwd1, prop1);
            StringAssert.Contains(prop1Response, data1, "The command 'get' must retrieve decrypted values.");

            // Retrieve first key-value pair and assert match.
            string prop2Response = get(clientPath1, serverPath, pwd1, prop2);
            Assert.IsFalse(prop2Response.Contains(data2), "The command 'get' must not retrieve dropped values.");
        }


        ///
        /// Domain-specific language (DSL) to simplify testing:
        ///

        private string init(string clientPath, string serverPath, string pwd)
        {
            resetConsoleOutput();
            setConsoleInput(pwd);
            run("init", clientPath, serverPath);
            return getConsoleOutput().Trim();
        }

        private string extractSecretKey(string clientPath)
        {
            resetConsoleOutput();
            run("secret", clientPath);
            return getConsoleOutput().Trim();
        }

        private string login(string clientPath, string serverPath, string pwd, string secret)
        {
            resetConsoleOutput();
            setConsoleInput(secret, pwd);
            run("login", clientPath, serverPath);
            return getConsoleOutput().Trim();
        }

        private string loginUsingRandomSecretKey(string clientPath, string serverPath, string pwd)
        {
            byte[] secret = new byte[16];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(secret);
            return login(clientPath, serverPath, pwd, Convert.ToBase64String(secret));
        }

        private string set(string clientPath, string serverPath, string pwd, string prop, string val)
        {
            resetConsoleOutput();
            setConsoleInput(val, pwd);
            run("set", clientPath, serverPath, prop);
            return getConsoleOutput().Trim();
        }

        private string get(string clientPath, string serverPath, string pwd, string prop)
        {
            resetConsoleOutput();
            setConsoleInput(pwd);
            run("get", clientPath, serverPath, prop);
            return getConsoleOutput().Trim();
        }

        private string getKeys(string clientPath, string serverPath, string pwd)
        {
            resetConsoleOutput();
            setConsoleInput(pwd);
            run("get", clientPath, serverPath);
            return getConsoleOutput().Trim();
        }

        private string set(string clientPath, string serverPath, string pwd, Dictionary<string, string> kvps)
        {
            string outputs = "";
            foreach (KeyValuePair<string, string> kvp in kvps)
            {
                outputs += Environment.NewLine;
                outputs += set(clientPath, serverPath, pwd, kvp.Key, kvp.Value);
            }
            return outputs;
        }

        private string drop(string clientPath, string serverPath, string pwd, string prop)
        {
            resetConsoleOutput();
            setConsoleInput(pwd);
            run("drop", clientPath, serverPath, prop);
            return getConsoleOutput().Trim();
        }

        private void run()
        {
            Program.Main(new string[] { });
        }

        private void run(string[] args)
        {
            Program.Main(args);
        }

        private void run(string a1)
        {
            Program.Main(new string[] { a1 });
        }

        private void run(string a1, string a2)
        {
            Program.Main(new string[] { a1, a2 });
        }

        private void run(string a1, string a2, string a3)
        {
            Program.Main(new string[] { a1, a2, a3 });
        }

        private void run(string a1, string a2, string a3, string a4)
        {
            Program.Main(new string[] { a1, a2, a3, a4 });
        }


        ///
        /// Console mocking
        ///


        private void setConsoleInput(string str)
        {
            Console.SetIn(new StringReader(str));
        }

        private void setConsoleInput(string l1, string l2)
        {
            setConsoleInput(new string[] { l1, l2 });
        }

        private void setConsoleInput(string[] lines)
        {
            string input = String.Join(Environment.NewLine, lines);
            Console.SetIn(new StringReader(input));
        }

        private void resetConsoleOutput()
        {
            this.output = new StringWriter();
            Console.SetOut(output);
        }

        private string getConsoleOutput()
        {
            return this.output.ToString();
        }

    }
}
