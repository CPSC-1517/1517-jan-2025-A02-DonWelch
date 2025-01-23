using OOPsReview;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

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

        [Fact]
        public void Create_An_Instance_Using_The_Greedy_Constructor_With_Address_Employements()
        {
            //Arrange (this is the setup of values needed for doing the test)
            string expectedFirstName = "Don";
            string expectedLastName = "Welch";

            ResidentAddress expectedAddres = new ResidentAddress(123, "Maple St.",
                                    "Edmonton", "AB", "T6Y7U8");

            //how to test a collection?
            //create individual instances of the item in the list
            //in this example those instances are objects
            //you must remember each object has a unique GUID
            //NOTE: you CANNOT reuse a single variable to hold the separate instances
            Employment one = new Employment("PG I", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2013/10/04"), 6.5);
            Employment two = new Employment("PG II", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2020/04/04"));
            List<Employment> employments = new(); //in .Net Core, one does not need to
                                                  //    specific the constructor of your class
                                                  //    on the new command as the system assumes
                                                  //    it is of the same datatype as the
                                                  //    declaration
            employments.Add(one);
            employments.Add(two);

            int expectedEmploymentPositionCount = 2;

            //Act ( this is the action of the test
            //sut: subject under test
            Person sut = new Person("Don", "Welch", expectedAddres, employments);

            //Assert (this checks the results of the Act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().Be(expectedAddres);
            //before testing the actual contents of your collections, it is best
            //  to check the number of items in the collection
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionCount);
            //did the greedy constructor actually use the data I submitted
            //were the instances in the list loaded as expected, including the expected order
            //check the actual contents of the list
            sut.EmploymentPositions.Should().ContainInConsecutiveOrder(employments);
        }
        #endregion
        #region Exception tests
        //check for a first name missing data (validation)
        //data value could be null
        //data value could be empty string
        //data value could be blank string

        //the second test anontation used is called [Theory]
        //it will execute n number of times as a loop
        //n is determind by the number [InlineData()] anontations following the [Theory]
        //to setup the test header, you must include a parameter in a parameter list
        //  one for each, value in the InlineData set of values
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Throw_Exception_Creating_Instance_Missing_FirstName(string testvalue)
        {
            //Arrange
            //possible values for FirstName: null, empty string, blank string
            //the setup of an exception test does not have to be as extensive as a successful test
            //  as the objective is to catch the exception that is thrown
            //in this example there will be no need to check expected values

            //Act
            //the act in this case is the capture of the exception that has been thrown
            //use () => to indicate that the following delegate is to be executed as the required code
            Action action = () => new Person(testvalue, "Welch", null, null);

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }


        //check for a last name missing data (validation)
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Throw_Exception_Creating_Instance_Missing_LastName(string testvalue)
        {
            //Arrange
            
            //Act
            Action action = () => new Person("Don", testvalue, null, null);

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }

        //check for a name missing data (validation)
        [Theory]
        [InlineData(null,"Welch")]
        [InlineData("", "Welch")]
        [InlineData("     ", "Welch")]
        [InlineData("Don",null)]
        [InlineData("Don", "")]
        [InlineData("Don", "     ")]
        public void Throw_Exception_Creating_Instance_Missing_First_OR_Last_Name(string firstname, string lastName)
        {
            //Arrange

            //Act
            Action action = () => new Person(firstname, lastName, null, null);

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }
        #endregion
        #endregion

        #region Properties
        #region Successful Tests
        //directly change firstname
        [Fact]
        public void Directly_Change_FirstName_Via_Property()
        {
            //Arrange
            string expectedFirstName = "Bob";
            Person sut = new Person();
            //Person sut = new Person("Don", "Welch", null, null);

            //Act
            sut.FirstName = "Bob";

            //Assert
            sut.FirstName.Should().Be(expectedFirstName);
        }
        //directly change lastname
        [Fact]
        public void Directly_Change_LastName_Via_Property()
        {
            //Arrange
            string expectedLastName = "Ujest";
            //Person sut = new Person();
            Person sut = new Person("Shirley", "Welch", null, null);

            //Act
            sut.FirstName = "Bob";

            //Assert
            sut.FirstName.Should().Be(expectedLastName);
        }
        //directly change Address (with new address or null)
        //consider making EmploymentPositions private set (must use method)
        //full name should return the name using the current instance data (last, first)
        #endregion
        #region Exception Tests
        //check firstname has data (ArgumentNUllException)
        //check lastname has data (ArgumentNUllException)
        #endregion
        #endregion

        #region Methods
        #region Successful Tests
        #endregion
        #region Exception Tests
        #endregion
        #endregion
    }
}