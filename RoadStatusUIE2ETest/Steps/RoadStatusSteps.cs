using Reqnroll;

namespace RoadStatusUIE2ETest.Steps
{
    [Binding]
    public class RoadStatusSteps
    {
        private StringWriter strWriter = new StringWriter();
        private string? _roadNumber;
        private int? _exitCode;
        
        [Given(@"a valid road ""(.*)"" is specified")]
        public void SetRoadNumber(string  roadNumber)
        {
            _roadNumber = roadNumber;
        }

        [Given(@"an invalid road ""(.*)"" is specified")]
        public void SetInValidRoadNumber(string roadNumber)
        {
            _roadNumber = roadNumber;
        }
        [When("the client is run")]
        public async Task WhenTheClientIsRun()
        {
            Console.SetOut(strWriter);
            _exitCode  = await RoadStatusUI.Program.Main([_roadNumber]);
        }

        [When("the client is run without road name")]
        public async Task WhenTheClientIsRunWithoutRoadName()
        {
            Console.SetOut(strWriter);
            _exitCode = await RoadStatusUI.Program.Main([]);
        }

        [Then("the road name should be displayed as: {string}")]
        public void ThenTheRoadNameShouldBeDisplayedas(string result)
        {
            Assert.Contains(result, strWriter.ToString());
        }

        [Then("the road status severity should be displayed as: {string}")]
        public void ThenTheRoadStatusSeverityShouldBeDisplayedas(string result)
        {
            Assert.Contains(result, strWriter.ToString());
        }

        [Then("the road status severity description should be displayed as: {string}")]
        public void ThenTheRoadStatusSeverityDescriptionShouldBeDisplayedas(string result)
        {
            Assert.Contains(result, strWriter.ToString());
        }

        [Then("the application should return the message: {string}")]
        public void ThenTheApplicationShouldReturnTheMessage(string result)
        {
            Assert.Contains(result, strWriter.ToString());
        }

        [Then(@"the exit code should be ""(.*)""")]
        public void SetInValidRoadNumber(int exitCode)
        {
            Assert.Equal(exitCode, _exitCode);
        }

    }
}