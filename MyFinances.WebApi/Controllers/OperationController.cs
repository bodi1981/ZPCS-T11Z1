using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Converters;
using MyFinances.WebApi.Models.Domains;
using MyFinances.WebApi.Models.DTOs;
using MyFinances.WebApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public OperationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<DataResponse<IEnumerable<OperationDto>>> GetOperations()
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                var data = await _unitOfWork.Operation.GetOperations();
                response.Data = data.ToDtos();
            }
            catch (Exception ex)
            {
                //logowanie dfo pliku...
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }

        [HttpGet("{records:int:min(1)}/{pageNo:int:min(1)}")]
        public async Task<DataResponse<IEnumerable<OperationDto>>> GetOperationsByPage(int records, int pageNo)
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                var data = await _unitOfWork.Operation.GetOperationsbyPage(records, pageNo);
                response.Data = data.ToDtos();
            }
            catch (Exception ex)
            {
                //logowanie dfo pliku...
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        /// <summary>
        /// Get operation by id
        /// </summary>
        /// <param name="id">operation id</param>
        /// <returns>DataResponse - OperationDto</returns>
        [HttpGet("{id}")]
        public async Task<DataResponse<OperationDto>> GetOperation(int id)
        {
            var response = new DataResponse<OperationDto>();

            try
            {
                var data = await _unitOfWork.Operation.GetOperation(id);
                response.Data = data?.ToDto();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }

        [HttpPost]
        public async Task<DataResponse<int>> AddOperation(OperationDto operationDto)
        {
            var response = new DataResponse<int>();

            try
            {
                var operation = operationDto.ToDao();
                await _unitOfWork.Operation.AddOperation(operation);
                await _unitOfWork.Complete();
                response.Data = operation.Id;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }

        [HttpPut]
        public async Task<Response> UpdateOperation(OperationDto operation)
        {
            var response = new Response();

            try
            {
                await _unitOfWork.Operation.UpdateOperation(operation.ToDao());
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<Response> RemoveOperation(int id)
        {
            var response = new Response();

            try
            {
                await _unitOfWork.Operation.RemoveOperation(id);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }
    }
}
