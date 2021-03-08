using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Dto
{
    public class CreateTokenDto
    {
        public int Id { get; private set; }

        public CreateTokenDto(int id)
        {
            Id = id;
        }
    }
}
