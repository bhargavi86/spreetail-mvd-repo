using Spreetail.MVD.Services.Implementations;

namespace Spreetail.Test
{
    public class CommandServiceTests
    {
        [Fact]
        public void Add_Member_ReturnsTrue()
        {
            //Arrange
            var commandService = new CommandService();

            //Act
            commandService.ADD("foo", "bar");
            var keyExists = commandService.KEYEXISTS("foo");
            var memberExists = commandService.MEMBEREXISTS("bar");

            //Assert
            Assert.True(keyExists);
            Assert.True(memberExists);
        }

        [Fact]
        public void Remove_Member_ReturnsFalse()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");

            //Act
            commandService.REMOVE("foo", "bar");
            var keyExists = commandService.KEYEXISTS("foo");
            var memberExists = commandService.MEMBEREXISTS("bar");

            //Assert
            Assert.False(keyExists);
            Assert.False(memberExists);
        }

        [Fact]
        public void Keys_ReturnsAllKeys()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");
            commandService.ADD("sweet", "laddu");

            //Act
            var keys = commandService.KEYS();

            //Assert
            Assert.NotEmpty(keys);
            Assert.Contains("foo", keys);
            Assert.Contains("sweet", keys);
            Assert.DoesNotContain("bar", keys);
            Assert.DoesNotContain("laddu", keys);
        }

        [Fact]
        public void Members_ByKey_ReturnsAllMembers()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");
            commandService.ADD("foo", "marz");

            //Act
            var members = commandService.MEMBERS("foo");

            //Assert
            Assert.NotEmpty(members);
            Assert.Contains("bar", members);
            Assert.Contains("marz", members);
            Assert.DoesNotContain("foo", members);
        }

        [Fact]
        public void RemoveAll_ByKey_RemovesKeyMemberPair()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");
            commandService.ADD("foo", "marz");

            //Act
            var result = commandService.REMOVEALL("foo");
            var items = commandService.ITEMS();

            //Assert
            Assert.True(result);
            Assert.Null(items);
        }

        [Fact]
        public void Clear_RemovesAllKeyMemberPairs()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");
            commandService.ADD("foo", "marz");
            commandService.ADD("sweet", "chocolate");

            //Act
            commandService.CLEAR();
            var items = commandService.ITEMS();

            //Assert
            Assert.Null(items);
        }

        [Fact]
        public void KeyExists_HasKey_ReturnsTrue()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");

            //Act
            var keyExists = commandService.KEYEXISTS("foo");

            //Assert
            Assert.True(keyExists);
        }

        [Fact]
        public void KeyExists_DoesNotHaveKey_ReturnsFalse()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");

            //Act
            var keyExists = commandService.KEYEXISTS("sweet");

            //Assert
            Assert.False(keyExists);
        }

        [Fact]
        public void MemberExists_HasMember_ReturnsTrue()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");

            //Act
            var keyExists = commandService.MEMBEREXISTS("bar");

            //Assert
            Assert.True(keyExists);
        }

        [Fact]
        public void MemberExists_DoesNotHaveMember_ReturnsFalse()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");

            //Act
            var keyExists = commandService.MEMBEREXISTS("marz");

            //Assert
            Assert.False(keyExists);
        }

        [Fact]
        public void AllMembers_ReturnsAllMembers()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");
            commandService.ADD("foo", "marz");
            commandService.ADD("sweet", "chocolate");

            //Act
            var allMembers = commandService.ALLMEMBERS();

            //Assert
            Assert.NotEmpty(allMembers);
            Assert.Contains("bar", allMembers);
            Assert.Contains("marz", allMembers);
            Assert.Contains("chocolate", allMembers);
            Assert.DoesNotContain("foo", allMembers);
            Assert.DoesNotContain("sweet", allMembers);
        }

        [Fact]
        public void Items_ReturnsAllKeyMemberPairs()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "bar");
            commandService.ADD("foo", "marz");
            commandService.ADD("sweet", "chocolate");

            //Act
            var items = commandService.ITEMS();

            //Assert
            Assert.NotEmpty(items);
        }

        [Fact]
        public void Sort_ReturnsAllKeyMemberPairs_InAlphabeticalOrder()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("sweet", "chocolate");
            commandService.ADD("foo", "bar");
            commandService.ADD("foo", "marz");

            //Act
            var items = commandService.SORT();

            //Assert
            Assert.NotEmpty(items);
            if (items != null)
            {
                Assert.True(items.Select(x => x.Key).First() == "foo");
            }
        }

        [Fact]
        public void SortMembers_ByKey_ReturnsMembers_InAlphabeticalOrder()
        {
            //Arrange
            var commandService = new CommandService();
            commandService.ADD("foo", "marz");
            commandService.ADD("foo", "bar");

            //Act
            var members = commandService.SORTMEMBERS("foo");

            //Assert
            Assert.NotEmpty(members);
            if (members != null)
            {
                Assert.True(members.First() == "bar");
            }
        }
    }
}