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
        public DataResponse<IEnumerable<OperationDto>> GetOperations()
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                response.Data = _unitOfWork.Operation.GetOperations().ToDtos();
            }
            catch (Exception ex)
            {
                //logowanie dfo pliku...
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }

        [HttpGet("{records:int:min(1)}/{pageNo:int:min(1)}")]
        public DataResponse<IEnumerable<OperationDto>> GetOperationsByPage(int records, int pageNo)
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                response.Data = _unitOfWork.Operation.GetOperationsbyPage(records, pageNo).ToDtos();
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
        public DataResponse<OperationDto> GetOperation(int id)
        {
            var response = new DataResponse<OperationDto>();

            try
            {
                response.Data = _unitOfWork.Operation.GetOperation(id)?.ToDto();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }

        [HttpPost]
        public DataResponse<int> AddOperation(OperationDto operationDto)
        {
            var response = new DataResponse<int>();

            try
            {
                var operation = operationDto.ToDao();
                _unitOfWork.Operation.AddOperation(operation);
                _unitOfWork.Complete();
                response.Data = operation.Id;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }

        [HttpPut]
        public Response UpdateOperation(OperationDto operation)
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.UpdateOperation(operation.ToDao());
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }

        [HttpDelete("{id}")]
        public Response RemoveOperation(int id)
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.RemoveOperation(id);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }
    }
}
