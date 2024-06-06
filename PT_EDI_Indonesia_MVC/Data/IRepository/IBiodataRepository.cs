using PT_EDI_Indonesia_MVC.Core.ViewModels;

namespace PT_EDI_Indonesia_MVC.Data.IRepository
{
    public interface IBiodataRepository
    {
        public Task<List<BiodataVM>> GetBiodatasAsync();

    }
}