using Spreetail.MVD.Models;

namespace Spreetail.MVD.Services.Interfaces
{
    public interface ICommandService
    {
        bool ADD(string key, string member);

        bool? REMOVE(string key, string member);

        List<string>? KEYS();

        List<string>? MEMBERS(string key);

        bool REMOVEALL(string key);

        void CLEAR();

        bool KEYEXISTS(string key);

        bool MEMBEREXISTS(string member);

        List<string> ALLMEMBERS();

        List<KeyMemberPair>? ITEMS();

        List<KeyMemberPair>? SORT();

        List<string>? SORTMEMBERS(string key);
    }
}
