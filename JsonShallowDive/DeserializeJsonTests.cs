using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ToJSON;
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

            string json = @"{'companies' : {'abc' : 'Abc Company', 'def' : 'Def Company'}}";
            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            _output.WriteLine(data.ToJSON());
            //http://www.newtonsoft.com/json/help/html/DeserializeDictionary.htm
            //{
            //  "abc": "Abc Company",
            //  "def": "Def Company"
            //}
        }

        [Fact]
        public void deserialize_with_additional_key_to_dictionary_string_string() {

            string json2 = @"{'abc':'Abc Company','def':'Def Company','ghi':'Ghi Company'}";
            Dictionary<string, string> data2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(json2);

            _output.WriteLine(data2.ToJSON());
            //{
            //  "abc": "Abc Company",
            //  "def": "Def Company",
            //  "ghi": "Ghi Company"
            //}
        }

        [Fact]
        public void deserialize_to_linq_to_json_jobject() {

            string json = @"{'companies': {'abc' : 'Abc Company', 'def' : 'Def Company', 'ghi' : 'Ghi Company' }}";
            var parent = JObject.Parse(json);

            //_output.WriteLine(data.ToJSON());
            //http://www.newtonsoft.com/json/help/html/QueryingLINQtoJSON.htm
            //{
            //  "companies": {
            //  "abc": "Abc Company",
            //  "def": "Def Company",
            //  "ghi": "Ghi Company"
            // }
            //}

            //For illustration purposes
            var companies = parent.Value<JObject>("companies").Properties();

            foreach (var company in companies) {
                _output.WriteLine(company.Name + " : " + company.Value);
            }
            //abc : Abc Company
            //def : Def Company
            //ghi : Ghi Company

            var companiesDict = companies
                .ToDictionary(
                    k => k.Name,
                    v => v.Value.ToString());

            _output.WriteLine(companiesDict.GetType().ToString());
            //System.Collections.Generic.Dictionary`2[System.String,System.String]

            _output.WriteLine(companiesDict.ToJSON());
            //{
            //  "abc": "Abc Company",
            //  "def": "Def Company",
            //  "ghi": "Ghi Company"
            //}
        }
    }
}
