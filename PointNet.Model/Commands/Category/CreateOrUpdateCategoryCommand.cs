using System;
using PointNet.CommandProcessor.Command;
using PointNet.Model;
using System.Collections.Generic;

namespace PointNet.Model.Commands
{
    public class CreateOrUpdateCategoryCommand : ICommand
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
