using PointNet.CommandProcessor.Command;

namespace PointNet.Model.Commands
{
    public class DeleteExpenseCommand : ICommand
    {
        public int ExpenseId { get; set; }
    }
}
