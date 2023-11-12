using Moq;
using StudentAPI.Abstraction.IGPA;
using StudentAPI.Features.GPACalculator;
namespace StudentApi.Test
{
    public class StudentGpaTest
    {
        private Mock<IGPARepository> _repositoryMock;
        

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IGPARepository>();
        }

        [Test]
        public async Task GPA_Service_Calculator_Test()
        {
            int studentid = 1;
           
            List<int> subjectId = new List<int>() {1, 2, 3 };
            List<int> credits = new List<int>() { 3, 5, 6 };
            List<int> points = new List<int>() { 91, 91, 91 };
            _repositoryMock.Setup(s => s.GetSubjectCreditsAsync(studentid)).ReturnsAsync(credits);
            _repositoryMock.Setup(s => s.GetStudentSubjectIdListAsync(studentid)).ReturnsAsync(subjectId);
            for(int i =0; i < subjectId.Count; i++) 
            {
                _repositoryMock.Setup(s => s.GetSubjectCreditAsync(studentid, subjectId[i])).ReturnsAsync(credits[i]);
                _repositoryMock.Setup(s => s.GetStudentPointAsync(studentid, subjectId[i])).ReturnsAsync(points[i]);
            }
            var service = new GPAService(_repositoryMock.Object);

            var result = await service.CalculateGpaAsync(studentid);
            double expectedValue = 4.0;
            Assert.AreEqual(expectedValue, result);
            //_repositoryMock.Verify( s => s.GetSubjectCreditAsync(It.IsAny<int>(), It.IsAny<int>()));
        }
    }
}