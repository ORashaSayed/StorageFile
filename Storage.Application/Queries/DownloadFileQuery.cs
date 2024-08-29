using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Queries
{
    public class DownloadFileQuery : IRequest<byte[]>
    {
        public int FileId { get; }

        public DownloadFileQuery(int fileId)
        {
            FileId = fileId;
        }
    }

}
