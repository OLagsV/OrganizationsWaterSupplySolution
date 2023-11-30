namespace OrganizationsWaterSupplyL4.Services
{
    public interface ICachedService<T> where T : class
    {
        public IEnumerable<T> GetData(int rowsNumber = 20);
        public void AddData(string cacheKey, int rowsNumber = 20);
        public IEnumerable<T> GetData(string cacheKey, int rowsNumber = 20);
        public void UpdateData(string cacheKey, int rowsNumber = 20);
    }
}
