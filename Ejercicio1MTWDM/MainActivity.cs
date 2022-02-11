using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ejercicio1MTWDM
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnDescargar;
        Button btnExtraer;
        ImageView Imagen;
        EditText txtNombre;
        string ruta;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Hide();
            btnDescargar = FindViewById<Button>(Resource.Id.btndescargar);
            btnExtraer = FindViewById<Button>(Resource.Id.btncargar);
            Imagen = FindViewById<ImageView>(Resource.Id.imagen);
            txtNombre = FindViewById<EditText>(Resource.Id.txtnombre);
            btnDescargar.Click += ColocarImagen;
            btnExtraer.Click += delegate
            {
                var carpeta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var archivo = txtNombre.Text + ".jpg";
                ruta = Path.Combine(carpeta, archivo);
                Android.Net.Uri rutaImagen = Android.Net.Uri.Parse(ruta);
                Imagen.SetImageURI(rutaImagen);
            };
        }
        async void ColocarImagen(object sender, EventArgs e)
        {
            var path = await DownloadImage();
            Android.Net.Uri rutaImagen = Android.Net.Uri.Parse(path);
            Imagen.SetImageURI(rutaImagen);
        }
        public async Task<string> DownloadImage()
        {
            var cliente = new System.Net.WebClient();
            byte[] datosImagen = await cliente.DownloadDataTaskAsync("https://pbs.twimg.com/media/EnTx1AHUwAIUGwl.jpg");
            var carpeta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var archivo = txtNombre.Text + ".jpg";
            ruta = Path.Combine(carpeta, archivo);
            File.WriteAllBytes(ruta, datosImagen);
            return ruta;
        }
    }
}



