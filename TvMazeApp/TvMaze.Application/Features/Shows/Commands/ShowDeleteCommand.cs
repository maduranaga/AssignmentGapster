using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvMaze.Application.Features.Shows.Commands
{
    public class ShowDeleteCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
