﻿namespace MefContrib.Tools.Visualizer.Models
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.ComponentModel.Composition.Diagnostics;

    public class PartInfo : NotifyObject
    {
        private PartDefinitionInfo _partDefinitionInfo;

        public PartInfo(PartDefinitionInfo partDefinitionInfo)
        {
            this._partDefinitionInfo = partDefinitionInfo;
        }

        public string DisplayName
        {
            get
            {
                return CompositionElementTextFormatter.DisplayCompositionElement(this._partDefinitionInfo.PartDefinition);
            }
        }

        public bool IsRejected
        {
            get
            {
                return this._partDefinitionInfo.IsRejected;
            }
        }

        public bool IsPrimaryRejection
        {
            get
            {
                return this._partDefinitionInfo.IsPrimaryRejection;
            }
        }

        public string Description
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                if (this.IsPrimaryRejection)
                {
                    builder.Append("[Primary Rejection] ");
                }
                else if (this.IsRejected)
                {
                    builder.Append("[Rejected] ");
                }

                builder.Append(CompositionElementTextFormatter.DescribeCompositionElement(this._partDefinitionInfo.PartDefinition));

                return builder.ToString();
            }
        }

        public string FullDescription
        {
            get
            {
                using (StringWriter writer = new StringWriter())
                {
                    PartDefinitionInfoTextFormatter.Write(this._partDefinitionInfo, writer);

                    return writer.ToString();
                }
            }
        }

        public IEnumerable<ExportInfo> Exports
        {
            get
            {
                var exports = from x in this._partDefinitionInfo.PartDefinition.ExportDefinitions
                              select new ExportInfo(x);

                return exports;
            }
        }

        public IEnumerable<ImportInfo> Imports
        {
            get
            {
                var imports = from x in this._partDefinitionInfo.ImportDefinitions
                              select new ImportInfo(x);

                return imports;
            }
        }
    }
}