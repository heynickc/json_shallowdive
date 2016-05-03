using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace JsonShallowDive {

    public class DeserializeJsonTests {
        private readonly ITestOutputHelper _output;

        public DeserializeJsonTests(ITestOutputHelper output) {
            _output = output;
        }

        [Fact]
        public void deserialize_to_dictionary_string_string() {

            string json = @"{'abc' : 'Abc Company', 'def' : 'Def Company'}";
            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            _output.WriteLine(data.ToJson());
            //http://www.newtonsoft.com/json/help/html/DeserializeDictionary.htm
            //{
            //  "abc": "Abc Company",
            //  "def": "Def Company"
            //}
        }

        [Fact]
        public void deserialize_with_additional_key_to_dictionary_string_string() {

            string json2 = @"{'abc' : 'Abc Company', 'def' : 'Def Company', 'ghi' : 'Ghi Company' }";
            Dictionary<string, string> data2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(json2);

            _output.WriteLine(data2.ToJson());
            //{
            //  "abc": "Abc Company",
            //  "def": "Def Company",
            //  "ghi": "Ghi Company"
            //}
        }

        [Fact]
        public void deserialize_to_linq_to_json_jobject() {

            string json3 = @"{'companies': {'abc' : 'Abc Company', 'def' : 'Def Company', 'ghi' : 'Ghi Company' }}";
            JObject data3 = JObject.Parse(json3);

            _output.WriteLine(data3.ToJson());
            //http://www.newtonsoft.com/json/help/html/QueryingLINQtoJSON.htm
            //{
            //  "companies": {
            //  "abc": "Abc Company",
            //  "def": "Def Company",
            //  "ghi": "Ghi Company"
            // }
            //}
            _output.WriteLine(data3["companies"]["abc"].ToJson());
            // Abc Company
            _output.WriteLine(data3.SelectToken("companies").Values().ToJson());
            //http://www.newtonsoft.com/json/help/html/SelectToken.htm
            //[
            //  "Abc Company",
            //  "Def Company",
            //  "Ghi Company"
            //]
        }
    }
}
