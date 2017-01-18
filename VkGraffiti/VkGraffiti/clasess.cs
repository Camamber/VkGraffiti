using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VkGraffiti
{
   

    #region Get.Friends JSON Clasess

    public class Item
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo_50 { get; set; }
        public int online { get; set; }
        public string online_app { get; set; }
        public int online_mobile { get; set; }
        public List<int?> lists { get; set; }
        public string deactivated { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
    }

    public class RootObject
    {
        public Response response { get; set; }
    }

    #endregion

    #region Get.Server JSON
    public class ResponseGet
    {
        public string upload_url { get; set; }    
    }

    public class JSON_GetServer
    {
        public ResponseGet response { get; set; }
    }
    #endregion

    #region Doc JSON

    public class DocObject
    {
        public string file { get; set; }
    }



    public class Size
    {
        public string src { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string type { get; set; }
    }

    public class Photo
    {
        public List<Size> sizes { get; set; }
    }

    public class Video
    {
        public string src { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int file_size { get; set; }
    }

    public class Graffiti
    {
        public string src { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Preview
    {
        public Photo photo { get; set; }
        public Video video { get; set; }
        public Graffiti graffiti { get; set; }
    }

    public class ResponseDoc
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public int size { get; set; }
        public string ext { get; set; }
        public string url { get; set; }
        public int date { get; set; }
        public int type { get; set; }
        public Preview preview { get; set; }
    }

    public class DocInformation
    {
        public List<ResponseDoc> response { get; set; }
    }

    #endregion

    public class Error
    {
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public List<Object> request_params { get; set; }
    }

    public class ErrorObject
    {
        public Error error { get; set; }
    }







    public class ItemMe
    {
        public int id { get; set; }
        public int owner_id { get; set; }
    }

    public class ResponseMe
    {
        public int count { get; set; }
        public List<ItemMe> items { get; set; }
    }

    public class Me
    {
        public ResponseMe response { get; set; }
    }


    public class OwnResponse
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo_50 { get; set; }
        public int online { get; set; }
    }

    public class Own
    {
        public List<OwnResponse> response { get; set; }
    }
}

