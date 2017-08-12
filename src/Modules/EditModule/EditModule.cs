﻿using System.Collections;
using System.Collections.Generic;
using EditModule.Commands;
using EditModule.Features;
using EditModule.Features.SyntaxHighlighting;
using EditModule.Models;
using EditModule.Views;
using ICSharpCode.AvalonEdit;
using Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace EditModule
{
    public class EditModule : IModule
    {
        public IUnityContainer Container { get; }
        public IRegionManager RegionManager { get; }

        public EditModule(IUnityContainer container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;
        }

        public void Initialize()
        {
            Container.RegisterType<IBlockBackgroundRenderer, BlockBackgroundRenderer>();
            Container.RegisterType<ITextEditorComponent, TextEditor>();

            Container.RegisterType<IEditFeature, Features.TextEditorOptions>(nameof(Features.TextEditorOptions));
            Container.RegisterType<IEditFeature, SyntaxHighlighting>(nameof(SyntaxHighlighting));
            Container.RegisterType<IEnumerable<IEditFeature>, IEditFeature[]>();

            Container.RegisterType<IEditCommandHandler, NewCommandHandler>(nameof(NewCommandHandler));
            Container.RegisterType<IEditCommandHandler, OpenCommandHandler>(nameof(OpenCommandHandler));
            Container.RegisterType<IEditCommandHandler, SaveCommandHandler>(nameof(SaveCommandHandler));
            Container.RegisterType<IEditCommandHandler, SaveAsCommandHandler>(nameof(SaveAsCommandHandler));
            Container.RegisterType<IEnumerable<IEditCommandHandler>, IEditCommandHandler[]>();

            RegionManager.RegisterViewWithRegion(Constants.EditRegion, typeof(EditControl));
        }
    }
}
