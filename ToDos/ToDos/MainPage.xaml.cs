using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models;
using Xamarin.Forms;
using ToDos.Data;
using System.IO;
using Xamarin.Essentials;

namespace ToDos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //var toDos = new List<ToDo>()
            //{
            //    new ToDo(){Title="Xamarin", Description="Nauczyć się Xamarin"},
            //    new ToDo(){Title="Sieci", Description="Nauczyć się Cisco"}
            //};
            
            var toDos = await App.DB.getToDosAsync(); // Lista z SQLite


            var response = await new Services.ToDosRestService().Client.GetAsync("https://todosbackend2019.azurewebsites.net/api/ToDos/List");  // pobranie z chmury

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var toDosCloudJSON = await response.Content.ReadAsStringAsync(); // wyciągniecie zawartości
                toDos.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<ToDo>>(toDosCloudJSON));
            }



            toDos.ForEach(td => td.Image = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), td.Image + ".jpg"));

            lvToDos.ItemsSource = ToDoSGroups.ToDosGroupsList(toDos);
        }

        async private void LvToDos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //await Flashlight.TurnOnAsync().ContinueWith(
            //    (ca) =>
            //    {
            //        System.Threading.Thread.Sleep(5000);
            //        Flashlight.TurnOffAsync().RunSynchronously();
            //    }
            //    );

            try
            {
                await Flashlight.TurnOnAsync();
                System.Threading.Thread.Sleep(3000);
                await Flashlight.TurnOffAsync();
                Vibration.Vibrate(3000);
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Błąd 1", ex.Message, "ok");
            }
            catch (PermissionException ex)
            {
                await DisplayAlert("Błąd 2", ex.Message, "ok");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException;
                while(inner.InnerException != null)
                {
                    inner = inner.InnerException;
                }
                await DisplayAlert("Błąd 2", inner.Message, "ok");
            }

            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ToDoPage()
                {
                    BindingContext = e.SelectedItem as ToDo
                });
            }


        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.DB.addOrUpdateToDoAsync(new ToDo() { Title = "Wykonać telefon", Description = "Zadzwonić do kierownik dziekanatu" });
            App.DB.addOrUpdateToDoAsync(new ToDo() { Title = "Wykonać telefon nr 2", Description = "Zadzwonić do kierownika katedry" });
            App.DB.addOrUpdateToDoAsync(new ToDo() { Title = "Zarezerować pokój w hotelu", Description = "Zadzwonić do hotelu, aby zarezerwować pokój dwuosobowy" });
        }

        async private void AddToDo_Clicked(object sender, EventArgs e)
        {
            ToDo nowy = new ToDo();
            await Navigation.PushAsync(new AddToDoPage2()
            {
            });
        }
    }
}
