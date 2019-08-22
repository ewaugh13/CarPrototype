﻿using Microsoft.Xna.Framework;
using GeonBit;
using GeonBit.ECS;
using GeonBit.ECS.Components;
using GeonBit.ECS.Components.Graphics;
using GeonBit.ECS.Components.Misc;
using GeonBit.ECS.Components.Particles;
using GeonBit.ECS.Components.Physics;
using GeonBit.ECS.Components.Sound;
using System.Diagnostics;

using CarProto.Custom_Components;
using Microsoft.Xna.Framework.Graphics;

namespace CarProto
{
    /// <summary>
    /// Your main game class!
    /// </summary>
    internal class Game1 : GeonBitGame
    {
        GameObject shapeObject;

        /// <summary>
        /// Initialize your GeonBitGame properties here.
        /// </summary>
        public Game1()
        {
            InitParams.UiTheme = "hd";
            InitParams.DebugMode = true;
            InitParams.EnableVsync = true;
            InitParams.Title = "New GeonBit Project";
            InitParams.FullScreen = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        override public void Update(GameTime gameTime)
        {
            /// exit application on escape
            if (Managers.GameInput.IsKeyDown(GeonBit.Input.GameKeys.Escape))
            {
                Exit();
            }

            /// TBD add any custom Update functionality here.
        }

        /// <summary>
        /// Initialize to implement per main type.
        /// </summary>
        override public void Initialize()
        {
            /// TBD create your scene, components and init resources here.
            /// The code below contains a simple example of how to use UI, camera, and basic entity renderer.

            /// Example 1: create UI text
            ActiveScene.UserInterface.AddEntity(new GeonBit.UI.Entities.Paragraph("Welcome to GeonBit! Here's a sphere:"));

            /// Example 3: add 3d shape to scene
            shapeObject = new GameObject("shape");
            Model carModel = Resources.GetModel("Models/MuscleCar");
            shapeObject.AddComponent(new ModelRenderer(carModel));
            shapeObject.AddComponent(new PlayerController());
            shapeObject.SceneNode.Rotation = new Vector3(0f * (MathHelper.Pi / 180), 270f * (MathHelper.Pi / 180), 270f * (MathHelper.Pi / 180));
            shapeObject.SceneNode.Scale = new Vector3(1f, 1f, 1f);
            shapeObject.Parent = ActiveScene.Root;

            GameObject backgroundObject = new GameObject("background");
            Texture2D backgroundimage = Resources.GetTexture("Images/SpyHunter");
            SceneBackground background = new SceneBackground(backgroundimage)
            {
                DrawMode = BackgroundDrawMode.Cover
            };
            backgroundObject.AddComponent(background);
            backgroundObject.Parent = ActiveScene.Root;

            GameObject cameraObject = new GameObject("camera", SceneNodeType.Simple);
            cameraObject.AddComponent(new Camera());
            cameraObject.SceneNode.PositionZ = 100;
            cameraObject.Parent = ActiveScene.Root; //shapeObject

            // add diagnostic data paragraph to scene
            var diagnosticData = new GeonBit.UI.Entities.Paragraph("", GeonBit.UI.Entities.Anchor.BottomLeft, offset: Vector2.One * 10f, scale: 0.7f);
            diagnosticData.BeforeDraw = (GeonBit.UI.Entities.Entity entity) =>
            {
                diagnosticData.Text = Managers.Diagnostic.GetReportString();
            };
            ActiveScene.UserInterface.AddEntity(diagnosticData);
        }

        /// <summary>
        /// Draw function to implement per main type.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        override public void Draw(GameTime gameTime)
        {
            /// TBD add any custom drawing functionality here.
            /// Note: since GeonBit handle its own drawing internally, usually you don't need to do anything here.
        }
    }
}
