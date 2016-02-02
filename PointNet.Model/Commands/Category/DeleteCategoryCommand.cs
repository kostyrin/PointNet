using PointNet.CommandProcessor.Command;

namespace PointNet.Model.Commands
{
    public class DeleteCategoryCommand : ICommand
    {
        public int CategoryId { get; set; }
    }
}
