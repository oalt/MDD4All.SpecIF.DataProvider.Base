/*
 * Copyright (c) MDD4All.de, Dr. Oliver Alt
 */
using System.Collections.Generic;
using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataModels.Service;
using MDD4All.SpecIF.DataProvider.Contracts;

namespace MDD4All.SpecIF.DataProvider.Base
{
    public abstract class AbstractSpecIfMetadataReader : ISpecIfMetadataReader
    {
        public ISpecIfServiceDescription DataSourceDescription { get; set; }

        public List<EnumerationValue> GetEnumOptions(string dataTypeID)
        {
            List<EnumerationValue> result = new List<EnumerationValue>();

            DataType dataType = GetDataTypeByKey(new Key(dataTypeID, null));

            if (dataType != null && dataType.Enumeration != null)
            {
                foreach (EnumerationValue value in dataType.Enumeration)
                {
                    result.Add(value);
                }
            }

            return result;
        }

        public List<string> GetDataTypeTypes()
        {
            List<string> result = new List<string>()
            {
                "xs:boolean", "xs:integer", "xs:double", "xs:dateTime", "xs:string", "xs:anyURL"
            };

            return result;
        }

        public abstract List<DataType> GetAllDataTypes();
        public abstract List<PropertyClass> GetAllPropertyClasses();
        public abstract List<ResourceClass> GetAllResourceClasses();
        public abstract DataType GetDataTypeByKey(Key key);
        public abstract PropertyClass GetPropertyClassByKey(Key key);
        public abstract ResourceClass GetResourceClassByKey(Key key);
        public abstract StatementClass GetStatementClassByKey(Key key);
        public abstract string GetLatestPropertyClassRevision(string propertyClassID);
        public abstract string GetLatestResourceClassRevision(string resourceClassID);
        public abstract string GetLatestStatementClassRevision(string statementClassID);
        public abstract List<StatementClass> GetAllStatementClasses();
        public abstract List<DataType> GetAllDataTypeRevisions(string dataTypeID);
        public abstract List<PropertyClass> GetAllPropertyClassRevisions(string propertyClassID);
        public abstract List<ResourceClass> GetAllResourceClassRevisions(string resourceClassID);
        public abstract List<StatementClass> GetAllStatementClassRevisions(string statementClassID);
        public abstract void NotifyMetadataChanged();
        public abstract List<DiagramObjectClass> GetAllDiagramObjectClasses();
        public abstract DiagramObjectClass GetDiagramObjectClassByKey(Key key);
        public abstract List<DiagramObjectClass> GetAllDiagramObjectClassesRevisions(string classID);
    }
}
