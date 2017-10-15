namespace UnitTestProject.users
{
    class GetUserResponse
    {
        //        {
        //    "data": {
        //        "id": 2,
        //        "first_name": "Janet",
        //        "last_name": "Weaver",
        //        "avatar": "https://s3.amazonaws.com/uifaces/faces/twitter/josephstein/128.jpg"
        //    }
        //}
        public Data data;

        public class Data
        {
            public string id;
            public string first_name;
            public string last_name;
            public string avatar;

        }
    }
}
