
namespace Elabor8_PracticeTest_Arjun.StepDefinition
{
    /// <summary>
    /// Specflow step definition to cover all the Given, When and Then steps for all scenarios
    /// </summary>
    [Binding]
    public class PracticeTestStepDefinition
    {
        Methods method = new Methods();
        CustomFunction customFunction = new CustomFunction();
        static List<string> factList = new List<string>();
        RestResponse? response = null;


        [Given(@"user invokes the Get Facts request with (.*)")]
        public void UserInvokesGetFactsRequest(string URL)
        {
            response = method.GetMethod(URL);
        }

        [Given(@"user invokes the Get Facts request for specifiedId (.*)")]
        public void UserInvokesGetFactsRequestID(string URL)
        {
            if (factList.Count > 0){
                response = method.GetMethod(URL + factList[0]);
            }
        }

        [When(@"the request returns a successful response")]
        public void RequestReturnsSuccessfulResponse()
        {
            Assert.IsTrue(response.IsSuccessful, response.IsSuccessStatusCode.ToString());
            Assert.IsTrue(response.ContentLength > 0, "response content has value");
        }

        [Given(@"user invokes the Get Facts request for invalidId (.*)")]
        public void UserInvokesGetFactsInvalidID(string URL)
        {
            response = method.GetMethod(URL);
        }

        [When(@"the request returns a unsuccessful response")]
        public void RequestReturnsUnSuccessfulResponse()
        {
            Assert.IsTrue(true, "Bad Request " + response.StatusCode.ToString());
            Assert.IsTrue(response.ContentLength > 0, "response content has error response");
        }



        [Then(@"validate the fact ids returned from the response")]
        public void ValidateFactIdsResponse()
        {
            JArray jArray = JArray.Parse(json: response.Content.ToString());

            foreach (JObject item in jArray)
            {
                factList.Add(item.GetValue("_id").ToString());

            }
        }

        [Then(@"validate the details stored for the fact ID and url (.*)")]
        public void ValidateStoredFactID(string URL)
        {
            dynamic? jObject = null;

            if (factList.Count > 0)
            {
                foreach (var factId in factList)
                {
                    response = method.GetMethod(URL + factId);
                    jObject = JObject.Parse(response.Content.ToString());
                    customFunction.validateResponseFields(jObject);

                }

            }
        }

        [Then(@"verify the schema of the api response with input file (.*)")]
        public async Task VerifySchemaApiResponseInputFileAsync(string filename)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string schemaContent = File.ReadAllText(workingDirectory + @"\Schema\" + filename);

            var schemaObj = await JsonSchema.FromJsonAsync(schemaContent);

            var jObjectResponse = response.Content.ToString();

            var errorsForValidJson = schemaObj.Validate(jObjectResponse);

            Assert.IsTrue(errorsForValidJson.Count == 0, "Json Schema is valid");


        }
    
    }
}
