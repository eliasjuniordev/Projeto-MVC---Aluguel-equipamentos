﻿using AluguelEquipamentos.Negocio.Dtos;
using AluguelEquipamentos.Negocio.Interfaces;
using AluguelEquipamentos.Negocio.Models;
using System.Dynamic;
using System.Text.Json;

namespace AluguelEquipamentos.Data.Client
{
    public class ApiBrasilClient:IApiCep
    {
        public async Task<ResponseGenerico<EnderecoModel>> BuscarEnderecoPorCEP(string cep)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://brasilapi.com.br/api/cep/v1/{cep}");
            var response = new ResponseGenerico<EnderecoModel>();

            using var client = new HttpClient();
            var responseBrasilApi = await client.SendAsync(request);
            var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();

            var objResponse = JsonSerializer.Deserialize<EnderecoModel>(contentResp);

            if (responseBrasilApi.IsSuccessStatusCode)
            {
                response.CodigoHttp = responseBrasilApi.StatusCode;
                response.DadosRetorno = objResponse;

            }
            else
            {
                response.CodigoHttp = responseBrasilApi.StatusCode;
                response.ErroRetorno = JsonSerializer.Deserialize<ExpandoObject>(contentResp);
            }

            return response;
        }
    }
}
