﻿namespace Pacyfist.CrudGenerator.Dtos
{
    public class ModelDto
    {
		public string? ModelNamespace { get; internal set; }

        public string? BaseNamespace { get; internal set; }

		public string? SingularName { get; internal set; }

        public string? PluralName { get; internal set; }
    }
}
