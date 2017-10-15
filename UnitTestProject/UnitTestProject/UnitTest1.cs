using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System;
using UnitTestProject.users;
using UnitTestProject.registration;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        string BaseUrl = "";
        [TestInitialize]
        public void SetUP()
        {
            BaseUrl = " ";
            //string url = string.Format("{0}/name?PrimaryName={1}", System.Configuration.ConfigurationManager.AppSettings["URLREST"], "yournmae");
            //Console.WriteLine(details);
        }

        [TestMethod]
        public void TestUserMovies()
        {
            //setting the url
            string url = "https://reqres.in/api/users";
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);

            //setting the headers
            webrequest.Method = "POST";
            webrequest.ContentType = "application/json";

            //Request
            //    name: "paul rudd",
            //    movies: ["I Love You Man", "Role Models"]

            //Serialize
            UserRequest userRequest = new UserRequest();
            userRequest.name = "paul rudd";
            userRequest.movies = new string[] { "I Love You Man", "Role Models" };

            //Prepare Request body by using a stream of bytes 
            using (var streamWriter = new StreamWriter(webrequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(userRequest);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            //Response
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);

            //Send Request and receive Response in a string
            string result = responseStream.ReadToEnd();
            webresponse.Close();

            Console.WriteLine(result);

            //Deserialize
            UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(result);
            Console.WriteLine(userResponse.id);

            //Tests
            Assert.AreEqual(201, (int)webresponse.StatusCode);
            Assert.IsNotNull(userResponse.id);
            Assert.IsNotNull(userResponse.createdAt);
            Assert.AreEqual(userRequest.name, userResponse.name, message: "name mismatch");
            Assert.AreEqual(userRequest.movies.Length, userResponse.movies.Length, message: "movies length mismatch");
            Assert.AreEqual(userRequest.movies[0], userResponse.movies[1], message: "movies 0 mismatch");
            Assert.AreEqual(userRequest.movies[1], userResponse.movies[1], message: "movies 1 mismatch");

        }

        [TestMethod]
        public void TestRegistration()
        {
            string url = "https://reqres.in/api/register";
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);

            //setting the headers
            webrequest.Method = "POST";
            webrequest.ContentType = "application/json";

            //Serializing request object
            RegistrationRequest registrationRequest = new RegistrationRequest();
            registrationRequest.email = "sydney@fife";
            registrationRequest.password = "pistol";

            //Prepare Request body by using a stream of bytes 
            using (var streamWriter = new StreamWriter(webrequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(registrationRequest);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            //Response
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);

            //Send Request and receive Response in a string
            string result = responseStream.ReadToEnd();
            webresponse.Close();

            //Deserialize response
            RegistrationResponse registrationResponse = JsonConvert.DeserializeObject<RegistrationResponse>(result);

            Assert.AreEqual(201, (int)webresponse.StatusCode);
            Assert.IsNotNull(registrationResponse.token);
        }

        [TestMethod]
        public void TestGetUser()
        {
            string url = "https://reqres.in/api/users/1";
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);

            //setting the headers
            webrequest.Method = "GET";
            webrequest.ContentType = "application/json";

            //Response
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);

            //Send Request and receive Response in a string
            string result = responseStream.ReadToEnd();
            webresponse.Close();

            //Deserialize response
            GetUserResponse getUserResponse = JsonConvert.DeserializeObject<GetUserResponse>(result);

            Assert.AreEqual(200, (int)webresponse.StatusCode);
            Assert.IsNotNull(getUserResponse.data.id);
            Assert.AreEqual("George", getUserResponse.data.first_name, message: "First Name mismatch");
            Assert.AreEqual("Bluth", getUserResponse.data.last_name, message: "Last Name mismatch");
            Assert.AreEqual("https://s3.amazonaws.com/uifaces/faces/twitter/calebogden/128.jpg", getUserResponse.data.avatar, message: "avatar mismatch");

        }

    }

}
