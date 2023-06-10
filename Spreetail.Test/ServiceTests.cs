using Microsoft.Extensions.Configuration;
using Spreetail.MVD.Services.Implementations;
using Spreetail.MVD.Services.Interfaces;

namespace Spreetail.Test
{
    public class CommandServiceTests
    {
        private IConfiguration _configuration;
        private ICommandService _commandService;

        public CommandServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Application:MaxKeyCount", "3"},
                {"Application:MaxMemberCount", "3"}
            };
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            _commandService = new CommandService(_configuration);
        }

        [Fact]
        public void Add_Member_ReturnsTrue()
        {
            //Act
            _commandService.ADD("foo", "bar");
            var keyExists = _commandService.KEYEXISTS("foo");
            var memberExists = _commandService.MEMBEREXISTS("foo", "bar");

            //Assert
            Assert.True(keyExists);
            Assert.True(memberExists);
        }

        [Fact]
        public void Remove_Member_ReturnsFalse()
        {
            //Arrange
            _commandService.ADD("foo", "bar");

            //Act
            _commandService.REMOVE("foo", "bar");
            var keyExists = _commandService.KEYEXISTS("foo");
            var memberExists = _commandService.MEMBEREXISTS("foo", "bar");

            //Assert
            Assert.False(keyExists);
            Assert.False(memberExists);
        }

        [Fact]
        public void Keys_ReturnsAllKeys()
        {
            //Arrange
            _commandService.ADD("foo", "bar");
            _commandService.ADD("sweet", "laddu");

            //Act
            var keys = _commandService.KEYS();

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
            _commandService.ADD("foo", "bar");
            _commandService.ADD("foo", "marz");

            //Act
            var members = _commandService.MEMBERS("foo");

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
            _commandService.ADD("foo", "bar");
            _commandService.ADD("foo", "marz");

            //Act
            var result = _commandService.REMOVEALL("foo");
            var items = _commandService.ITEMS();

            //Assert
            Assert.True(result);
            Assert.Null(items);
        }

        [Fact]
        public void Clear_RemovesAllKeyMemberPairs()
        {
            //Arrange
            _commandService.ADD("foo", "bar");
            _commandService.ADD("foo", "marz");
            _commandService.ADD("sweet", "chocolate");

            //Act
            _commandService.CLEAR();
            var items = _commandService.ITEMS();

            //Assert
            Assert.Null(items);
        }

        [Fact]
        public void KeyExists_HasKey_ReturnsTrue()
        {
            //Arrange
            _commandService.ADD("foo", "bar");

            //Act
            var keyExists = _commandService.KEYEXISTS("foo");

            //Assert
            Assert.True(keyExists);
        }

        [Fact]
        public void KeyExists_DoesNotHaveKey_ReturnsFalse()
        {
            //Arrange
            _commandService.ADD("foo", "bar");

            //Act
            var keyExists = _commandService.KEYEXISTS("sweet");

            //Assert
            Assert.False(keyExists);
        }

        [Fact]
        public void MemberExists_HasMember_ReturnsTrue()
        {
            //Arrange
            _commandService.ADD("foo", "bar");

            //Act
            var keyExists = _commandService.MEMBEREXISTS("foo", "bar");

            //Assert
            Assert.True(keyExists);
        }

        [Fact]
        public void MemberExists_DoesNotHaveMember_ReturnsFalse()
        {
            //Arrange
            _commandService.ADD("foo", "bar");

            //Act
            var keyExists = _commandService.MEMBEREXISTS("foo","marz");

            //Assert
            Assert.False(keyExists);
        }

        [Fact]
        public void AllMembers_ReturnsAllMembers()
        {
            //Arrange
            _commandService.ADD("foo", "bar");
            _commandService.ADD("foo", "marz");
            _commandService.ADD("sweet", "chocolate");

            //Act
            var allMembers = _commandService.ALLMEMBERS();

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
            _commandService.ADD("foo", "bar");
            _commandService.ADD("foo", "marz");
            _commandService.ADD("sweet", "chocolate");

            //Act
            var items = _commandService.ITEMS();

            //Assert
            Assert.NotEmpty(items);
        }

        [Fact]
        public void Sort_ReturnsAllKeyMemberPairs_InAlphabeticalOrder()
        {
            //Arrange
            _commandService.ADD("sweet", "chocolate");
            _commandService.ADD("foo", "bar");
            _commandService.ADD("foo", "marz");

            //Act
            var items = _commandService.SORT();

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
            _commandService.ADD("foo", "marz");
            _commandService.ADD("foo", "bar");

            //Act
            var members = _commandService.SORTMEMBERS("foo");

            //Assert
            Assert.NotEmpty(members);
            if (members != null)
            {
                Assert.True(members.First() == "bar");
            }
        }
    }
}