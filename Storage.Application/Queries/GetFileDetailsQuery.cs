using MediatR;
using Storage.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Queries
{
    public class GetFileDetailsQuery : IRequest<FileDto>
    {
        public int FileId { get; }

        public GetFileDetailsQuery(int fileId)
        {
            FileId = fileId;
        }
    }

}
