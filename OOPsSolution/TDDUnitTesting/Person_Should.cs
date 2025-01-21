using OOPsReview;
using FluentAssertions;

namespace TDDUnitTesting
{
    public class Person_Should
    {
        #region Constructors
        #region Successful
        //the [Fact] unit test executes once
        //without the [Fact] annotation, the method is NOT considered a unit test
        [Fact]
        public void Create_An_Instance_Using_The_Default_Constructor()
        {
            //Arrange (this is the setup of values needed for doing the test)
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionCount = 0;

            //Act ( this is the action of the test
            //sut: subject under test
            Person sut = new Person();

            //Assert (this checks the results of the Act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionCount);

        }

        [Fact]
        public void Create_An_Instance_Using_The_Greedy_Constructor_With_NO_Address_Employements()
        {
            //Arrange (this is the setup of values needed for doing the test)
            string expectedFirstName = "Don";
            string expectedLastName = "Welch";
            int expectedEmploymentPositionCount = 0;

            //Act ( this is the action of the test
            //sut: subject under test
            Person sut = new Person("Don","Welch",null,null);

            //Assert (this checks the results of the Act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionCount);

        }

        public void Create_An_Instance_Using_The_Greedy_Constructor_With_Address_Employements()
        {
            //Arrange (this is the setup of values needed for doing the test)
            string expectedFirstName = "Don";
            string expectedLastName = "Welch";

            ResidentAddress expectedAddres = new ResidentAddress(123, "Maple St.",
                                    "Edmonton", "AB", "T6Y7U8");


            int expectedEmploymentPositionCount = 0;

            //Act ( this is the action of the test
            //sut: subject under test
            Person sut = new Person("Don", "Welch", null, null);

            //Assert (this checks the results of the Act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionCount);

        }
        #endregion
        #region Exception tests
        #endregion
        #endregion

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}