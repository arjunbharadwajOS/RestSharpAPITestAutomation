
namespace Elabor8_PracticeTest_Arjun.Libraries
{
    /// <summary>
    /// Custom Function class helps to validate the csv file with the response data
    /// </summary>
    public class CustomFunction
    {

        public void validateResponseFields(dynamic jObject)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string[] lines = File.ReadAllLines(workingDirectory + @"\TestData\ResponseData.csv");

            string[] columnHeader = lines[0].Split(',');

            int idIndex = 0, userIdIndex = 0, textIndex = 0, firstIndex = 0, lastIndex = 0, typeIndex = 0;

            for (int headerIndex = 0; headerIndex < columnHeader.Length; headerIndex++)
            {
                if (columnHeader[headerIndex].Equals("_id"))
                {
                    idIndex = headerIndex;
                }
                else if (columnHeader[headerIndex].Equals("user"))
                {
                    userIdIndex = headerIndex;
                }
                else if (columnHeader[headerIndex].Equals("text"))
                {
                    textIndex = headerIndex;
                }
                else if (columnHeader[headerIndex].Equals("user.name.first"))
                {
                    firstIndex = headerIndex;
                }
                else if (columnHeader[headerIndex].Equals("user.name.last"))
                {
                    lastIndex = headerIndex;
                }
                else if (columnHeader[headerIndex].Equals("type"))
                {
                    typeIndex = headerIndex;
                }
            }

            string[]? rowData = null;

            for (int lineIndex = 1; lineIndex < lines.Length; lineIndex++)
            {
                rowData = lines[lineIndex].Split(',');

                if (rowData[idIndex].Equals(Convert.ToString(jObject._id)))
                {
                    Assert.IsTrue(true, rowData[idIndex]);
                }
                else if (rowData[userIdIndex].Equals(Convert.ToString(jObject.user)))
                {
                    Assert.IsTrue(true, rowData[userIdIndex]);
                }
                else if (rowData[textIndex].Equals(Convert.ToString(jObject.text)))
                {
                    Assert.IsTrue(true, rowData[textIndex]);
                }
                else if (rowData[firstIndex].Equals(Convert.ToString(jObject.user.name.first)))
                {
                    Assert.IsTrue(true, rowData[firstIndex]);
                }
                else if (rowData[lastIndex].Equals(Convert.ToString(jObject.user.name.last)))
                {
                    Assert.IsTrue(true, rowData[lastIndex]);
                }
                else if (rowData[typeIndex].Equals(Convert.ToString(jObject.type)))
                {
                    Assert.IsTrue(true, rowData[typeIndex]);
                }
            }

        }


    }
}
