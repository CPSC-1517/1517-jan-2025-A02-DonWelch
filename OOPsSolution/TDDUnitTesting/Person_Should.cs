using OOPsReview;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using System.Net;

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
            sut.LastName = "Ujest";

            //Assert
            sut.LastName.Should().Be(expectedLastName);
        }
        //directly change Address (with new address or null)
        [Fact]
        public void Change_Their_Address_Via_Property()
        {
            //Arrange
            ResidentAddress expectedAddress = new ResidentAddress(321,"Ash Lane","Edmonton","AB","T5R4E3");
            //Person sut = new Person();
            Person sut = new Person("Shirley", "Ujest",
                new ResidentAddress(333, "Maple St.", "Edmonton", "AB", "T6Y7U8"), null);

            //Act
            sut.Address = new ResidentAddress(321, "Ash Lane", "Edmonton", "AB", "T5R4E3");

            //Assert
            sut.Address.Should().Be(expectedAddress);
        }
        [Fact]
        public void Change_Their_Address_Via_Property_To_Be_Empty()
        {
            //Arrange
         
            //Person sut = new Person();
            Person sut = new Person("Shirley", "Ujest",
                new ResidentAddress(333, "Maple St.", "Edmonton", "AB", "T6Y7U8"), null);

            //Act
            sut.Address = null;

            //Assert
            sut.Address.Should().BeNull();
        }
        //consider making EmploymentPositions private set (must use method)
        //  do we wish to allow the entire employment collection to be replaced?
        //  consider, is the mutator set to private?
        //      if so, direct altering is not possible (access trouble)
        //      if private, any code to actually attempt to use the mutator (set) will NOT even compile
        //  so even though you my have "brainstormed" this test, it is possible to determind that
        //      the test is unnecessary

        //full name should return the name using the current instance data (last, first)
        [Fact]
        public void Retreive_The_Full_Name_Via_Property()
        {
            //Arrange

            string expectedFullName = "Ujest, Shirley";
            Person sut = new Person("Shirley", "Ujest",null, null);

            //Act
            string fullname = sut.FullName;

            //Assert
            fullname.Should().Be(expectedFullName);
           // sut.FullName.Should().Be(expectedFullName); //combine act and assert
        }

        #endregion
        #region Exception Tests
        //check firstname has data (ArgumentNUllException)
        //possible values for parameter: null, empty string, blank string
        //we have 3 possible tests for the same property
        //solution:
        //a) write 3 separate tests that will be the same except for the value being tested
        //b) write one test that allows for the tested value to be altered
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        public void Throw_Exception_Directly_Changing_FirstName_Via_Property_Missing_Data(string testvalue)
        {
            //Arrange
            Person sut = new Person("Lowan","Behold",null,null);

            //Act
            Action action = () => sut.FirstName = testvalue;

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }

        //check lastname has data (ArgumentNUllException)
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        public void Throw_Exception_Directly_Changing_LastName_Via_Property_Missing_Data(string testvalue)
        {
            //Arrange
            Person sut = new Person("Lowan", "Behold", null, null);

            //Act
            Action action = () => sut.LastName = testvalue;

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }
        #endregion
        #endregion

        #region Methods
        #region Successful Tests
        //ability to add an new employment instance to the collection
        [Fact]
        public void Add_New_Employment_To_Collection()
        {
            //Arrange

            ResidentAddress address = new ResidentAddress(123, "Maple St.",
                                    "Edmonton", "AB", "T6Y7U8");

            Employment one = new Employment("PG I", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2013/10/04"), 6.5);
            TimeSpan days = DateTime.Today.AddDays(-1) - DateTime.Parse("2020/04/04");
            double years = Math.Round(days.Days / 365.2, 1);
            Employment two = new Employment("PG II", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2020/04/04"), years);
            List<Employment> employments = new(); //in .Net Core, one does not need to
                                                  //    specific the constructor of your class
                                                  //    on the new command as the system assumes
                                                  //    it is of the same datatype as the
                                                  //    declaration
            employments.Add(one);
            employments.Add(two);
            Person sut = new Person("Don", "Welch", address, employments);

            //setup the changes that will occur
            //during collection testing (if the collection has orignal items) you WILL need to
            //  setup a separate second collection to test against

            List<Employment> expectedEmployment = new List<Employment>();
            expectedEmployment.Add(one);
            expectedEmployment.Add(two);
            Employment three = new Employment("Sup I", SupervisoryLevel.Supervisor,
                           DateTime.Today, 0.0);

            int expectedEmploymentPositionCount = 3;

            //Act
            sut.AddEmployment(three);

            //Assert
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionCount);
            sut.EmploymentPositions.Should().ContainInConsecutiveOrder(expectedEmployment);
        }
        //ability to change the person's fullname at one time
        [Fact]
        public void Change_FullName()
        {
            //Arrange
            Person sut = new Person();
            string fullname = "Ujest, Shirley";

            //Act
            sut.ChangeFullName("Shirley", "Ujest");

            //Assert
            sut.FullName.Should().Be(fullname);
        }

        #endregion
        #region Exception Tests
        //changing person"s name:missing data
        [Theory]
        [InlineData(null,"Stew-Dent")]
        [InlineData("","Stew-Dent")]
        [InlineData("      ","Stew-Dent")]
        [InlineData("Ima",null)]
        [InlineData("Ima", "")]
        [InlineData("Ima", "    ")]
        public void Throw_Exception_Changing_FullName_With_Missing_Data(string firstname, string lastname)
        {
            //Arrange
            Person sut = new Person("Lowan", "Behold", null, null);

            //Act
            Action action = () =>  sut.ChangeFullName(firstname, lastname); 
                                   

            //Assert
            //one can test the contents of the error message being thrown
            //this isdone using the .WithMessage(string)
            //a substring of the error message can be checked using *.....* for the string
            //one can use string interpolation wiht the creation of your test string
            action.Should()
                    .Throw<ArgumentNullException>()
                    .WithMessage("*is required*");
        }

        //add new employment: missing data
        [Fact]
        public void Throw_Exception_Adding_Employment_History_With_Missing_Data()
        {
            //Arrange
            Person sut = new Person("Lowan", "Behold", null, null);

            //Act
            Action action = () => sut.AddEmployment(null);


            //Assert
           
            action.Should()
                    .Throw<ArgumentNullException>()
                    .WithMessage("*missing employment data*");
        }
        //add new employment : duplicate employment instance
        [Fact]
        public void Throw_Exception_When_Adding_Duplicate_Employment_History()
        {
            //Arrange
            ResidentAddress address = new ResidentAddress(123, "Maple St.",
                                    "Edmonton", "AB", "T6Y7U8");

            Employment one = new Employment("PG I", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2013/10/04"), 6.5);
            TimeSpan days = DateTime.Today.AddDays(-1) - DateTime.Parse("2020/04/04");
            double years = Math.Round(days.Days / 365.2, 1);
            Employment two = new Employment("PG II", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2020/04/04"), years);
            Employment three = new Employment("Sup I", SupervisoryLevel.Supervisor,
                          DateTime.Today, 0.0);
            List<Employment> employments = new(); //in .Net Core, one does not need to
                                                  //    specific the constructor of your class
                                                  //    on the new command as the system assumes
                                                  //    it is of the same datatype as the
                                                  //    declaration
            employments.Add(one);
            employments.Add(two);
            employments.Add(three);
            Person sut = new Person("Don", "Welch", address, employments);

            //Act
            Action action = () => sut.AddEmployment(three);

            //Assert
            action.Should()
                    .Throw<ArgumentException>()
                    .WithMessage($"*{three.Title} on {three.StartDate}*");
        }
        #endregion
        #endregion
    }
}