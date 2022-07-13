using LinkedInApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApp.Services
{
    public class ApiService
    {
        # region Cadena de conexion

        private string ApiUrl = "https://linkedinapijll.azurewebsites.net/";

        #endregion

        #region Metodos

        //Obtener la lista 
        public async Task<ApiResponse> GetDataAsync(string controller)
        {
            try
            {
                // Invocamos la webapi con get
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ApiUrl)
                };
                var response = await client.GetAsync(controller);
                var result = await response.Content.ReadAsStringAsync();

                // Validamos nuestro resultado
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                // Hubo un problema con el resultado
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = ex.Message
                };
            }
        }

        //Agregar
        public async Task<ApiResponse> PostDataAsync(string controller, object data)
        {
            try
            {
                // Serializamos nuestro objeto a json
                var serialize = JsonConvert.SerializeObject(data);
                var content = new StringContent(serialize, Encoding.UTF8, "application/json");

                // Invocamos post
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ApiUrl)
                };
                var response = await client.PostAsync(controller, content);
                //Aqui muestra el error
                var result = await response.Content.ReadAsStringAsync();

                // Validamos resultado
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                // Hubo un problema con la webapi
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = ex.Message
                };
            }
        }

        //Actualizar 
        public async Task<ApiResponse> PutDataAsync(string controller, object data,int id)
        {
            try
            {
                // Serializamos nuestro objeto a json
                var serialize = JsonConvert.SerializeObject(data);
                var content = new StringContent(serialize, Encoding.UTF8, "application/json");

                // Invocamos la webapi con el método put
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ApiUrl)
                };
                var response = await client.PutAsync($"{controller}/{id}", content);
                var result = await response.Content.ReadAsStringAsync();

                // Validamos nuestro resultado
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                // Hubo un problema con la webapi
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = ex.Message
                };
            }
        }

        //Eliminar
        public async Task<ApiResponse> DeleteDataAsync(string controller, int id)
        {
            try
            {
                // Invocamos el método delete
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ApiUrl)
                };
                var response = await client.DeleteAsync($"{controller}/{id}");
                var result = await response.Content.ReadAsStringAsync();

                // Validamos resultado
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                // Hubo un problema con la webapi
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = ex.Message
                };
            }
        }


        #endregion
    }
}
