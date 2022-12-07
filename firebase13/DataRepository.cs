using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firebase13
{
    internal class DataRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://test-django-65815-default-rtdb.europe-west1.firebasedatabase.app/");

        public async Task<bool> Save(Data data)
        {
            var d = await firebaseClient.Child(nameof(Data)).PostAsync(JsonConvert.SerializeObject(data));
            if (string.IsNullOrEmpty(d.Key))
            {
                return false;
            }
            return true;
        }

        public async Task<List<Data>> GetAll()
        {
            return (await firebaseClient.Child(nameof(Data)).OnceAsync<Data>()).Select(item => new Data
            {
                Email = item.Object.Email,
                Name = item.Object.Name,
                Image = item.Object.Image,
                Id = item.Key
            }).ToList();
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(Data) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<Data> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(Data) + "/" + id).OnceSingleAsync<Data>());
        }

        public async Task<bool> Update(Data data)
        {
            await firebaseClient.Child(nameof(Data) + "/" + data.Id).PutAsync(JsonConvert.SerializeObject(data));
            return true;
        }



    }
}
