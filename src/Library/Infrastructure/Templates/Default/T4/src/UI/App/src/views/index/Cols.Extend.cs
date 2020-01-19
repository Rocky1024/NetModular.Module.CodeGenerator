﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.UI.App.src.views.index
{
    public partial class Cols : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private ClassBuildModel _class;
        private List<PropertyBuildModel> _properties;

        public Cols(TemplateBuildModel model)
        {
            _model = model;
        }

        public bool IsGlobal => false;

        public void Save()
        {
            if (_model.Project.ClassList != null && _model.Project.ClassList.Any())
            {
                foreach (var classModel in _model.Project.ClassList)
                {
                    _class = classModel;
                    _properties = _class.PropertyList.Where(m => m.ShowInList).ToList();
                    var dir = Path.Combine(_model.RootPath, $"src/UI/{_model.Project.WebUIDicName}/src/views", _class.Name.FirstCharToLower(), "index");
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    //清空
                    GenerationEnvironment.Clear();

                    var content = TransformText();

                    var filePath = Path.Combine(dir, $"cols.js");
                    File.WriteAllText(filePath, content);
                }
            }
        }
    }
}
