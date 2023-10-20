using Code.Runtime.StaticData;

namespace Code.Runtime.Infrastructure.Services.StaticData
{
    internal interface IStaticDataService
    {
        void LoadAll();
        void LoadBookTypes();
        StaticBook GetBookData(string id);
    }
}