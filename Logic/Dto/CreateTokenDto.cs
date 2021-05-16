using Utils.Guards;

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
