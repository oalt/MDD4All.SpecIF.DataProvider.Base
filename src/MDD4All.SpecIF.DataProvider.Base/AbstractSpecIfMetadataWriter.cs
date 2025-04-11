/*
 * Copyright (c) MDD4All.de, Dr. Oliver Alt
 */
using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataProvider.Contracts;

namespace MDD4All.SpecIF.DataProvider.Base
{
    public abstract class AbstractSpecIfMetadataWriter : ISpecIfMetadataWriter
    {
        public abstract void AddDataType(DataType dataType);
        public abstract void AddDiagramObjectClass(DiagramObjectClass diagramObjectClass);
        public abstract void AddPropertyClass(PropertyClass propertyClass);
        public abstract void AddResourceClass(ResourceClass resourceClass);
        public abstract void AddStatementClass(StatementClass statementClass);
        public abstract void UpdateDataType(DataType dataType);
        public abstract void UpdateDiagramObjectClass(DiagramObjectClass diagramObjectClass);
        public abstract void UpdatePropertyClass(PropertyClass propertyClass);
        public abstract void UpdateResourceClass(ResourceClass resourceClass);
        public abstract void UpdateStatementClass(StatementClass statementClass);
    }
}
