using Spreetail.MVD.Models;
using Spreetail.MVD.Services.Interfaces;

namespace Spreetail.MVD.Services.Implementations
{
    public class CommandService : ICommandService
    {
        private List<KeyMemberPair> _dictionary;

        public CommandService()
        {
            _dictionary = new List<KeyMemberPair>();
        }

        public bool ADD(string key, string member)
        {
            var addResult = true;
            var keyExists = _dictionary.Exists(x => x.Key == key);

            if (keyExists)
            {
                var memberExists = _dictionary.Where(x => x.Key == key).First().Members?.Exists(y => y == member);

                if (!memberExists.HasValue)
                {
                    _dictionary.First(x => x.Key == key).Members = new List<string>() { member };                   
                }
                else
                {
                    if (memberExists.Value)
                    {
                        addResult = false;
                    }
                    else
                    {
                        _dictionary.First(x => x.Key == key).Members?.Add(member);
                    }
                }                
            }
            else
            {
                _dictionary.Add(new KeyMemberPair() { Key = key, Members = new List<string>() { member } });
            }

            return addResult;
        }

        public List<string> ALLMEMBERS()
        {
            var allMembers = new List<string>();
            var allMemberLists = _dictionary.Select(x => x.Members).ToList();

            if (allMemberLists.Any())
            {
                foreach(var memberList in allMemberLists)
                {
                    if (memberList != null)
                    {
                        allMembers.AddRange(memberList);
                    }
                }
            }

            return allMembers;
        }

        public void CLEAR()
        {
            _dictionary.Clear();
        }

        public List<KeyMemberPair>? ITEMS()
        {
            return _dictionary.Count > 0 ? _dictionary : null;
        }

        public bool KEYEXISTS(string key)
        {
            return _dictionary.Exists(x => x.Key == key);
        }

        public List<string>? KEYS()
        {
            var keys = _dictionary.Select(x => x.Key).ToList();
            return keys.Count > 0 ? keys : null;
        }

        public bool MEMBEREXISTS(string member)
        {
            var memberExists = false;
            var memberLists = _dictionary.Select(x => x.Members);
            foreach(var memberList in memberLists)
            {
                if(memberList != null)
                {
                    memberExists = memberList.Contains(member);
                    break;
                }
            }
            return memberExists;
        }

        public List<string>? MEMBERS(string key)
        {
            var keyPair = _dictionary.Where(x => x.Key == key).FirstOrDefault();
            return keyPair?.Members;
        }

        public bool? REMOVE(string key, string member)
        {
            bool? removeResult = true;

            var keyExists = _dictionary.Exists(x => x.Key == key);
            if (keyExists)
            {
                var memberExists = _dictionary.Where(x => x.Key == key).First().Members?.Exists(y => y == member);
                if (memberExists.HasValue && memberExists.Value)
                {
                    _dictionary.Where(x => x.Key == key).First().Members?.Remove(member);
                    var keyMemberPair = _dictionary.Where(x => x.Key == key).FirstOrDefault();
                    if(keyMemberPair?.Members?.Count == 0)
                    {
                        _dictionary.Remove(keyMemberPair);
                    }
                }
                else
                {
                    removeResult = false;
                }
            }
            else
            {
                removeResult = null;
            }

            return removeResult;
        }

        public bool REMOVEALL(string key)
        {
            var removeAllResult = true;

            var keyMemberPair = _dictionary.Where(x => x.Key == key).FirstOrDefault();
            if (keyMemberPair != null)
            {               
                _dictionary.Remove(keyMemberPair);
            }
            else
            {
                removeAllResult = false;
            }

            return removeAllResult;
        }

        public List<KeyMemberPair>? SORT()
        {
            return _dictionary.OrderBy(x => x.Key).ToList();
        }

        public List<string>? SORTMEMBERS(string key)
        {
            List<string>? memberList = null;

            var keyExists = _dictionary.Exists(x => x.Key == key);
            if (keyExists)
            {
                memberList = _dictionary.Where(x => x.Key == key).First().Members;
                if(memberList != null)
                {
                    memberList = memberList.OrderBy(x => x).ToList();
                }               
            }

            return memberList;
        }
    }
}
