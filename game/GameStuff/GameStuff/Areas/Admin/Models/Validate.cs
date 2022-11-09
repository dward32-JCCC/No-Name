using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GameStuff.Models
{
    public class Validate
    {
        private const string GenreKey = "validGenre";
        private const string DeveloperKey = "validDeveloper";
        private const string EmailKey = "validEmail";

        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public void CheckGenre(string genreId, Repository<Genre> data)
        {
            Genre entity = data.Get(genreId);
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" : 
                $"Genre id {genreId} is already in the database.";
        }
        public void MarkGenreChecked() => tempData[GenreKey] = true;
        public void ClearGenre() => tempData.Remove(GenreKey);
        public bool IsGenreChecked => tempData.Keys.Contains(GenreKey);

        public void CheckDeveloper(string DevName, string operation, Repository<Developer> data)
        {
            Developer entity = null; 
            if (Operation.IsAdd(operation)) {
                entity = data.Get(new QueryOptions<Developer> {
                    Where = a => a.DevName == DevName});
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" : 
                $"Developer {entity.FullName} is already in the database.";
        }
        public void MarkDeveloperChecked() => tempData[DeveloperKey] = true;
        public void ClearDeveloper() => tempData.Remove(DeveloperKey);
        public bool IsDeveloperChecked => tempData.Keys.Contains(DeveloperKey);
    }
}
