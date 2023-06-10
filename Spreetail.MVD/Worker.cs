using Spreetail.MVD.Services.Interfaces;
using Spreetail.MVD.Models;

namespace Spreetail.MVD
{
    public class Worker : BackgroundService
    {
        private readonly IHost _host;
        private readonly ICommandService _commandService;

        public Worker(IHost host, ICommandService commandService)
        {
            _host = host;
            _commandService = commandService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Welcome to the Multi-Value Dictionary App!");
            Console.WriteLine("Type MENU to see a list of commands.");
            Console.WriteLine("Type ESC to exit at anytime.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            var inputValue = string.Empty;
            string? key;
            string? member;
            do
            {
                Console.WriteLine();
                Console.Write("> ");
                inputValue = Console.ReadLine();

                try
                {
                    switch (inputValue)
                    {
                        case "ADD":
                            do
                            {
                                Console.WriteLine("Enter key value:");
                                key = Console.ReadLine();
                            }
                            while (string.IsNullOrEmpty(key));
                            do
                            {
                                Console.WriteLine("Enter member value:");
                                member = Console.ReadLine();
                            }
                            while (string.IsNullOrEmpty(member));
                            var isAdded = _commandService.ADD(key, member);
                            if (isAdded)
                            {
                                Console.WriteLine("Added Member");
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Member already exists for key");
                            }
                            break;
                        case "REMOVE":
                            do
                            {
                                Console.WriteLine("Enter key value:");
                                key = Console.ReadLine();
                            }
                            while (string.IsNullOrEmpty(key));
                            do
                            {
                                Console.WriteLine("Enter member value:");
                                member = Console.ReadLine();
                            }
                            while (string.IsNullOrEmpty(member));
                            var isRemoved = _commandService.REMOVE(key, member);
                            if (isRemoved.HasValue)
                            {
                                if (isRemoved.Value)
                                {
                                    Console.WriteLine("Removed Member");
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: Member does not exist");
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Key does not exist");
                            }
                            break;
                        case "KEYS":
                            var keysResult = _commandService.KEYS();
                            if (keysResult != null)
                            {
                                Console.WriteLine(FormatMembers(keysResult));
                            }
                            else
                            {
                                Console.WriteLine("There are no keys in the dictionary");
                            }
                            break;
                        case "MEMBERS":
                            do
                            {
                                Console.WriteLine("Enter key value:");
                                key = Console.ReadLine();
                            }
                            while (string.IsNullOrEmpty(key));
                            var membersResult = _commandService.MEMBERS(key);
                            if (membersResult == null)
                            {
                                Console.WriteLine("ERROR: Key does not exist");
                            }
                            else
                            {
                                Console.WriteLine(FormatMembers(membersResult));
                            }
                            break;
                        case "REMOVEALL":
                            do
                            {
                                Console.WriteLine("Enter key value:");
                                key = Console.ReadLine();
                            }
                            while (string.IsNullOrEmpty(key));
                            var isRemovedAll = _commandService.REMOVEALL(key);
                            if (isRemovedAll)
                            {
                                Console.WriteLine("This Key has been removed from the dictionary");
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Key does not exist");
                            }
                            break;
                        case "CLEAR":
                            _commandService.CLEAR();
                            break;
                        case "KEYEXISTS":
                            do
                            {
                                Console.WriteLine("Enter key value:");
                                key = Console.ReadLine();
                            }
                            while (string.IsNullOrEmpty(key));
                            var keyExistsResult = _commandService.KEYEXISTS(key);
                            Console.WriteLine(keyExistsResult);
                            break;
                        case "MEMBEREXISTS":
                            do
                            {
                                Console.WriteLine("Enter member value:");
                                member = Console.ReadLine();
                            }
                            while (string.IsNullOrEmpty(member));
                            var memberExistsResult = _commandService.MEMBEREXISTS(member);
                            Console.WriteLine(memberExistsResult);
                            break;
                        case "ALLMEMBERS":
                            var allMemberResult = _commandService.ALLMEMBERS();
                            if (allMemberResult != null)
                            {
                                Console.WriteLine(FormatMembers(allMemberResult));
                            }
                            else
                            {
                                Console.WriteLine("There are no members in the dictionary");
                            }
                            break;
                        case "ITEMS":
                            var itemsResult = _commandService.ITEMS();
                            if (itemsResult != null)
                            {
                                PrintKeyMemberPairs(itemsResult);
                            }
                            else
                            {
                                Console.WriteLine("There are no items in the dictionary");
                            }
                            break;
                        case "SORT":
                            var sortResult = _commandService.SORT();
                            if (sortResult != null)
                            {
                                PrintKeyMemberPairs(sortResult);
                            }
                            else
                            {
                                Console.WriteLine("There are no items in the dictionary");
                            }
                            break;
                        case "SORTMEMBERS":
                            do
                            {
                                Console.WriteLine("Enter key value:");
                                key = Console.ReadLine();
                            }
                            while (string.IsNullOrEmpty(key));
                            var sortMembersResult = _commandService.SORTMEMBERS(key);
                            if (sortMembersResult != null)
                            {
                                Console.WriteLine(FormatMembers(sortMembersResult));
                            }
                            else
                            {
                                Console.WriteLine("There are no members for this key");
                            }
                            break;
                        case "MENU":
                            PrintMenu();
                            break;
                        default:
                            Console.WriteLine("That is an invalid command, please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred. Message: {ex.Message} StackTrace: {ex.StackTrace}");
                }
            }
            while (inputValue != "ESC");

            await _host.StopAsync();
        }

        #region Helper Methods
        protected void PrintMenu()
        {
            Console.WriteLine("List of Commands:");
            Console.WriteLine("KEYS: Returns all keys in the dictionary");
            Console.WriteLine("MEMBERS: Returns collection of strings for the given key");
            Console.WriteLine("ADD: Adds a member to a collection for a given key");
            Console.WriteLine("REMOVE: Removes a member from a key");
            Console.WriteLine("REMOVEALL: Removes all members for a key and removes the key");
            Console.WriteLine("CLEAR: Removes all keys and members from the dictionary");
            Console.WriteLine("KEYEXISTS: Returns whether a key exists or not");
            Console.WriteLine("MEMBEREXISTS: Returns whether a member exists within a key");
            Console.WriteLine("ALLMEMBERS: Returns all members in the dictionary");
            Console.WriteLine("ITEMS: Returns all keys in the dictionary and all of their members");
            Console.WriteLine("SORT: Sorts dictionary alphabetically by keys");
            Console.WriteLine("SORTMEMBERS: Sorts members of a key alphabetically");
        }

        protected void PrintKeyMemberPairs(List<KeyMemberPair> keyMemberPairs)
        {
            foreach (var keyMember in keyMemberPairs)
            {
                if (keyMember.Members != null)
                {
                    var memberList = FormatMembers(keyMember.Members);
                    Console.WriteLine($"Key:{keyMember.Key} Members:{memberList}");
                }
                else
                {
                    Console.WriteLine($"Key:{keyMember.Key} Members:");
                }

            }
        }

        protected string FormatMembers(List<string> members)
        {
            var membersFormattedList = string.Empty;

            foreach (var member in members)
            {
                membersFormattedList += member + ";";
            }

            return membersFormattedList;
        }
        #endregion
    }
}