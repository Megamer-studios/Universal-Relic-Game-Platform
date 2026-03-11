// Signed by: Akumarin :3
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Security.Cryptography;

using URGP.Inventory;

namespace URGP
{
    public class Game1 : Game
    {
        private static readonly string encryptionPassphrase = "MANTIMANTIMANTIm";
        private static readonly int KeySize = 32;
        private static readonly int IvSize = 16;
        private static readonly int Iterations = 100_000;
        public string input;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public int Line = 1;
        public Texture2D diaBox;
        public Texture2D Cem1;
        public Texture2D Cem2;
        public Texture2D pixel;
        public Texture2D background;
        public Texture2D Cem3;
        public Texture2D Portrait;
        private SoundEffect _previousBGM;
        public SoundEffect soundEffect;
        public SoundEffectInstance BMGins;
        public SoundEffect BMG;
        public bool isQuestion = false;
        Rectangle diaRect;
        public string infoText = "";
        public string infoText2 = "";
       public bool AreColoursInverted = false;

        public bool debugMode = false;
        public string filePath = @"Dialogues/Dia1.dlg";
        public bool IsConsoleOpen = false;
        public string ConsoleText = "";
        private RenderTarget2D _renderTarget;
        private int _internalWidth = 1024;
        private int _internalHeight = 768;
        KeyboardState previousKeyboardState;
        public SpriteFont _font;
        public List<InventoryItem> PlayerInventory = new List<InventoryItem>();
        public bool HasItem(int itemId)
        {
            return PlayerInventory.Any(item => item.id == itemId);
        }
       public Vector2 topLeft = new Vector2(0, 0);
        public Vector2 topRight = new Vector2(1024 - 256, 0);
        public Vector2 bottomLeft = new Vector2(0, 768 - 512);
        public Vector2 bottomRight = new Vector2(1024 - 256, 768 - 512);
        public Vector2 bottomMid = new Vector2(400, 767 - 512);
        public Vector2 NewtopLeft = new Vector2(0, 0);
        public Vector2 NewtopRight = new Vector2(1024 - 256, 0);
        public Vector2 NewbottomLeft = new Vector2(0, 768 - 512);
        public Vector2 NewbottomRight = new Vector2(1024 - 256, 768 - 512);
        public Vector2 NewbottomMid = new Vector2(400, 767 - 512);

        private static bool keyPressed = false;
        private static bool keyPressed2 = false;
        Color semiTransparentBlack = new Color(0, 0, 0, 128);
        Color semiTransparentWhite = new Color(255, 255, 255, 189);
        public Color backgroundColor = Color.Black;
        public bool bgImg = true;
        MouseState currentMouse;
        MouseState previousMouse;

        public void ResetPositions()
        {
         
       NewtopLeft = new Vector2(0, 0);
         NewtopRight = new Vector2(1024 - 256, 0);
         NewbottomLeft = new Vector2(0, 768 - 512);
         NewbottomRight = new Vector2(1024 - 256, 768 - 512);
       NewbottomMid = new Vector2(400, 767 - 512);
            bottomLeft = Vector2.Lerp(bottomLeft, NewbottomLeft, 0.1f);
     
           
                bottomRight = Vector2.Lerp(bottomRight, NewbottomRight, 0.1f);
         
                bottomMid = Vector2.Lerp(bottomMid, NewbottomMid, 0.1f);
            

    topLeft = Vector2.Lerp(topLeft, NewtopLeft, 0.1f);

    topRight = Vector2.Lerp(topRight, NewtopRight, 0.1f);
}
        private Point GetVirtualMousePosition()
        {
            var screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            var screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

            float scale = Math.Min((float)screenWidth / _internalWidth, (float)screenHeight / _internalHeight);

            int offsetX = (screenWidth - (int)(_internalWidth * scale)) / 2;
            int offsetY = (screenHeight - (int)(_internalHeight * scale)) / 2;


            int virtualX = (int)((currentMouse.X - offsetX) / scale);
            int virtualY = (int)((currentMouse.Y - offsetY) / scale);

            return new Point(virtualX, virtualY);
        }

        public Game1()
        {
            Inventory.InventoryItems.InitializeItems(this);
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Universal Relic Game Platform";
            _graphics.HardwareModeSwitch = false;
        }

        protected override void Initialize()
        {
            Window.AllowUserResizing = false;

            var screenBounds = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

            _graphics.PreferredBackBufferWidth = screenBounds.Width;
            _graphics.PreferredBackBufferHeight = screenBounds.Height;
            _graphics.IsFullScreen = true;
            IsMouseVisible = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }
        private void HandleKeyEnter()
        {
            Line++;
            Progress.ProgressLines(this);
            infoText2 = "";
        }
        protected override void LoadContent()
        {
          
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _renderTarget = new RenderTarget2D(GraphicsDevice, _internalWidth, _internalHeight);

            diaRect = new Rectangle(0, 550, 1024, 250);
            BMG = Content.Load<SoundEffect>("AMachine");
            BMGins = BMG.CreateInstance();
            BMGins.IsLooped = true;
            BMGins.Volume = 0.7f;
            BMGins.Play();
            _previousBGM = BMG;


            _font = Content.Load<SpriteFont>("File");

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
       
            Cem1 = Content.Load<Texture2D>("Sprite2");
            Cem2 = Content.Load<Texture2D>("Empty");
            Cem3 = Content.Load<Texture2D>("Empty");
            background = Content.Load<Texture2D>("bg1");
            diaBox = Content.Load<Texture2D>("Diabox");
          
            Line1();
            Progress.ProgressLines(this);
            
        }
        private static void DeriveKeyAndIV(string passphrase, byte[] salt, out byte[] key, out byte[] iv)
        {
            using var rfc2898 = new Rfc2898DeriveBytes(passphrase, salt, Iterations, HashAlgorithmName.SHA256);
            key = rfc2898.GetBytes(KeySize);
            iv = rfc2898.GetBytes(IvSize);
        }

        protected override void Update(GameTime gameTime)
        {

          
            if (_previousBGM != BMG)
            {
               
                BMGins?.Stop();
                BMGins?.Dispose();

           
                BMGins = BMG.CreateInstance();
                BMGins.IsLooped = true;
                BMGins.Volume = 0.5f;
                BMGins.Play();

                _previousBGM = BMG; 
            }
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            Point mousePos = GetVirtualMousePosition();

            bool isLeftClick = currentMouse.LeftButton == ButtonState.Pressed &&
                               previousMouse.LeftButton == ButtonState.Released;

            if (bottomLeft != NewbottomLeft)
            {
                bottomLeft = Vector2.Lerp(bottomLeft, NewbottomLeft, 0.1f);
            }
            if (bottomRight != NewbottomRight)
            {
                bottomRight = Vector2.Lerp(bottomRight, NewbottomRight, 0.1f);
            }
            if (bottomMid != NewbottomMid)
            {
                bottomMid = Vector2.Lerp(bottomMid, NewbottomMid, 0.1f);
            }
            if (topLeft != NewtopLeft)
            {
                topLeft = Vector2.Lerp(topLeft, NewtopLeft, 0.1f);
            }
            if (topRight != NewtopRight)
            {
                topRight = Vector2.Lerp(topRight, NewtopRight, 0.1f);
            }


            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.LeftShift) && !IsConsoleOpen && !isQuestion)
            {

                HandleKeyEnter();


            }

            if (keyboardState.IsKeyDown(Keys.Space) && !keyPressed && !IsConsoleOpen && !isQuestion)
            {

                HandleKeyEnter();

                keyPressed = true;
            }
            if (isLeftClick && diaRect.Contains(mousePos) && !IsConsoleOpen && !isQuestion)
            {
                Line++;
                Progress.ProgressLines(this);
                infoText2 = "";
            }
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                IsConsoleOpen = false;
               
            }
           
            if (keyboardState.IsKeyUp(Keys.Space))
            {
                keyPressed = false;
            }
            if (keyboardState.IsKeyDown(Keys.OemTilde) && !keyPressed2)
            {
                ConsoleText = "";
                IsConsoleOpen = !IsConsoleOpen;
                keyPressed2 = true;

            }
            if (keyboardState.IsKeyUp(Keys.OemTilde))
            {
                keyPressed2 = false;
            }
            if (keyboardState.IsKeyDown(Keys.Enter) && IsConsoleOpen)
            {

                ConsoleCommands();

            }

            if (IsConsoleOpen)
            {
                if (keyboardState.IsKeyDown(Keys.Back) && ConsoleText.Length > 0)
                {
                    ConsoleText = ConsoleText[..^1];
                }
                foreach (Keys key in keyboardState.GetPressedKeys())
                {
                    if (previousKeyboardState.IsKeyUp(key))
                    {
                        if (key == Keys.Back && ConsoleText.Length > 0)
                        {
                            ConsoleText = ConsoleText[..^1];
                        }
                        else
                        {
                            char c = ConvertKeyToChar(key, keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift));
                            if (c != '\0') ConsoleText += c;
                        }
                    }
                }
            }

            Sequencer.SequenceLines(this);
            previousKeyboardState = keyboardState;

            base.Update(gameTime);
        }
        public void Line1()
        {
            BMG = Content.Load<SoundEffect>("AMachine");
           
            isQuestion = false;
            filePath = @"Dialogues/Dia1.dlg";
           AreColoursInverted = false;
            bgImg = true;
            Line = 1;
            backgroundColor = Color.Black;
            Cem1 = Content.Load<Texture2D>("Empty");
            Cem2 = Content.Load<Texture2D>("Empty");
            Cem3 = Content.Load<Texture2D>("Empty");
            topLeft = new Vector2(0, 0);
            topRight = new Vector2(1024 - 256, 0);
            bottomLeft = new Vector2(0, 768 - 512);
            bottomRight = new Vector2(1024 - 256, 768 - 512);
            bottomMid = new Vector2(400, 767 - 512);
            ResetPositions();

            Portrait = Content.Load<Texture2D>("Empty");
            background = Content.Load<Texture2D>("bg1");
            infoText = "";
            PlayerInventory.Clear();
            
            ConsoleText = "";
        

        }
        public void AddItemToInventory(int itemid, bool systematic)
        {
            try
            {
                var item = InventoryItems.Items.FirstOrDefault(x => x.id == itemid);
                if (item != null)
                {
                    PlayerInventory.Add(item);
                    if (systematic)
                    {
                        Console.WriteLine($"Item '{item.name}' added to inventory.");
                    }
                    else
                    {

                        infoText2 += $"\nItem '{item.name}' added to inventory.";
                    }

                }
            }
            catch (Exception ex)
            {
                ConsoleText = "Error adding item: " + ex.Message;
            }
        }
        public void RemoveItemFromInventory(int itemid)
        {
            try
            {
                var item = InventoryItems.Items.FirstOrDefault(x => x.id == itemid);
                if (item != null)
                {
                    PlayerInventory.Remove(item);


                }
            }
            catch (Exception ex)
            {
                ConsoleText = "Error removing item: " + ex.Message;
            }
        }
        private char ConvertKeyToChar(Keys key, bool shift)
        {

            if (key >= Keys.A && key <= Keys.Z)
                return (char)(shift ? key : key + 32);

            if (key >= Keys.D0 && key <= Keys.D9)
            {
                int num = key - Keys.D0;
                return shift ? ")!@#$%^&*("[num] : (char)('0' + num);
            }

            return '\0';
        }

        public void ConsoleCommands()
        {
            string input = ConsoleText.ToLower();
            if (input == "exit")
            {
                Exit();
            }
            else if (input == "reset")
            {
                Line1();
                Progress.ProgressLines(this);
                ConsoleText = "";
            }
            else if (input == "save")
            {
                var saveData = new SaveData
                {
                    Line = Line,
                    FilePath = filePath,
                    InventoryItemIds = PlayerInventory.Select(i => i.id).ToList(),
                    BottomMidX = NewbottomMid.X,
                    BottomMidY = NewbottomMid.Y,
                    BottomLeftX = NewbottomLeft.X,
                    BottomLeftY = NewbottomLeft.Y,
                    BottomRightX = NewbottomRight.X,
                    BottomRightY = NewbottomRight.Y,
         
                };

                string json = System.Text.Json.JsonSerializer.Serialize(saveData);
                byte[] salt = RandomNumberGenerator.GetBytes(16);
                DeriveKeyAndIV(encryptionPassphrase, salt, out byte[] key, out byte[] iv);

                using var aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;

                using var ms = new MemoryStream();
                ms.Write(salt);
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (var writer = new StreamWriter(cs))
                {
                    writer.Write(json);
                }

                File.WriteAllBytes(@"Saves/Save.dat", ms.ToArray());
                ConsoleText = "";

            }

            else if (input == "gamemode1")
            {
                debugMode = true;
                ConsoleText = "Debug mode enabled.";
            }
            else if (input == "gamemode0")
            {
                debugMode = false;
                ConsoleText = "Debug mode disabled.";
            }
            else if (input.StartsWith("lines"))
            {
                if (debugMode)
                {
                    string[] parts = input.Split('s');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int lineNumber))
                    {
                        Line = lineNumber;
                        infoText2 = "";
                        Progress.ProgressLines(this);
                        ConsoleText = $"Line set to {lineNumber}.";
                    }
                    else
                    {
                        ConsoleText = "Invalid line number.";
                    }
                }
            }
            else if (input.StartsWith("additem"))
            {
                if (debugMode)
                {
                    string[] parts = input.Split('m');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int itemid))
                    {
                        AddItemToInventory(itemid, true);
                        ConsoleText = $"Item with ID {itemid} added to inventory.";

                    }
                    else
                    {

                    }
                }
            }
            else if (input.StartsWith("delitem"))
            {
                if (debugMode)
                {
                    string[] parts = input.Split('m');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int itemid))
                    {
                        RemoveItemFromInventory(itemid);
                        ConsoleText = $"Item with ID {itemid} removed from inventory.";

                    }
                    else
                    {

                    }
                }
            }
            else if (input == "fullscreen")
            {
                if (debugMode)
                {

                    _graphics.IsFullScreen = !_graphics.IsFullScreen;
                    _graphics.ApplyChanges();

                }

            }
            else if (input.StartsWith("use"))
            {

                string[] parts = input.Split('e');
                if (parts.Length == 2 && int.TryParse(parts[1], out int itemid))
                {
                    var item = PlayerInventory.FirstOrDefault(x => x.id == itemid);
                    if (PlayerInventory.Contains(item))
                    {
                        ConsoleText = $"Used item {item.ToString()}";
                        item.Use();
                      
                        
                    }
                    //else
                    //{
                    //    ConsoleText = "You can't use that.";
                    //}
                }
                else
                {
                    ConsoleText = "Invalid item ID.";
                }

            }

            else if (input == "load")
            {
                byte[] encryptedData = File.ReadAllBytes(@"Saves/Save.dat");

                byte[] salt = encryptedData.Take(16).ToArray();
                byte[] cipherText = encryptedData.Skip(16).ToArray();

                DeriveKeyAndIV(encryptionPassphrase, salt, out byte[] key, out byte[] iv);

                using var aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;

                using var ms = new MemoryStream(cipherText);
                using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using var reader = new StreamReader(cs);
                string json = reader.ReadToEnd();

                try
                {
                    var saveData = System.Text.Json.JsonSerializer.Deserialize<SaveData>(json);
                    Line = saveData.Line;
                    filePath = saveData.FilePath;
                    PlayerInventory.Clear();
                    foreach (int id in saveData.InventoryItemIds)
                    {
                        var item = InventoryItems.Items.FirstOrDefault(x => x.id == id);
                        if (item != null)
                            PlayerInventory.Add(item);
                    }
                    NewbottomMid = new Vector2(saveData.BottomMidX, saveData.BottomMidY);
                    NewbottomLeft = new Vector2(saveData.BottomLeftX, saveData.BottomLeftY);
                    NewbottomRight = new Vector2(saveData.BottomRightX, saveData.BottomRightY);
                    Progress.ProgressLines(this);
                    ConsoleText = "";
                }
                catch
                {
                    ConsoleText = "Error loading save.";
                }

            }




     

      
            else if (input == "credits")
            {
                if (filePath != @"Dialogues/Dev.dlg")
                {
                    var saveData = new SaveData
                    {
                        Line = Line,
                        FilePath = filePath,
                        InventoryItemIds = PlayerInventory.Select(i => i.id).ToList(),
                        BottomMidX = NewbottomMid.X,
                        BottomMidY = NewbottomMid.Y,
                        BottomLeftX = NewbottomLeft.X,
                        BottomLeftY = NewbottomLeft.Y,
                        BottomRightX = NewbottomRight.X,
                        BottomRightY = NewbottomRight.Y,

                    };

                    string json = System.Text.Json.JsonSerializer.Serialize(saveData);
                    byte[] salt = RandomNumberGenerator.GetBytes(16);
                    DeriveKeyAndIV(encryptionPassphrase, salt, out byte[] key, out byte[] iv);

                    using var aes = Aes.Create();
                    aes.Key = key;
                    aes.IV = iv;

                    using var ms = new MemoryStream();
                    ms.Write(salt);
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs))
                    {
                        writer.Write(json);
                    }

                    File.WriteAllBytes(@"Saves/BC.dat", ms.ToArray());
                    ConsoleText = "";

                    filePath = @"Dialogues/Dev.dlg";
                    Line = 1;
                    Progress.ProgressLines(this);
                    infoText2 = "";
                }
            }
            else if (input == "loadbc")
            {
                if (debugMode)
                {
                    if (File.Exists(@"Saves/BC.dat"))
                    {
                        LBC();
                        Progress.ProgressLines(this);
                        ConsoleText = "BC Save loaded.";
                    }
                    else
                    {
                        ConsoleText = "No BC Save found.";
                    }
                }
                else
                {

                }

            }
            else if (input == "inventory")
            {
                
                string inventoryList = "Inventory:\n";
                if (PlayerInventory.Count == 0)
                {
                    inventoryList += "No items in inventory.";
                }
                else
                {
                    foreach (var item in PlayerInventory)
                    {
                        inventoryList += $"{item.name}, id: {item.id.ToString()}\n";
                    }
                }
                ConsoleText = inventoryList;


            }
           
            



        }
        public string ReadDialogue(string filePath, int line)
        {

            string[] lines1 = File.ReadAllLines(filePath);


            string[] modifiedLines = lines1.Select(lineText => lineText.Replace("\\n", "\n")).ToArray();


            if (line < 0 || line >= modifiedLines.Length)
            {
                line = 0;
                Line = 0;
            }





            return modifiedLines[line];
        }
        public string ReadSave(string location)
        {
            try
            {
                byte[] encryptedData = File.ReadAllBytes(location);
                byte[] salt = encryptedData.Take(16).ToArray();
                byte[] cipherText = encryptedData.Skip(16).ToArray();

                DeriveKeyAndIV(encryptionPassphrase, salt, out byte[] key, out byte[] iv);

                using var aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;

                using var ms = new MemoryStream(cipherText);
                using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using var reader = new StreamReader(cs);
                string json = reader.ReadToEnd();

                var saveData = System.Text.Json.JsonSerializer.Deserialize<SaveData>(json);

                if (saveData == null)
                    return "ERROR READING SAVE";

                string inventorySummary = saveData.InventoryItemIds.Count > 0
                    ? string.Join(", ", saveData.InventoryItemIds
                        .Select(id => InventoryItems.Items.FirstOrDefault(x => x.id == id)?.name ?? $"Unknown#{id}"))
                    : "No items";

                return $"Line: {saveData.Line}, File: {saveData.FilePath}, Inventory: [{inventorySummary}]";
            }
            catch
            {
                return "ERROR READING SAVE";
            }
        }
        public void LBC()
        {
            byte[] encryptedData = File.ReadAllBytes(@"Saves/BC.dat");

            byte[] salt = encryptedData.Take(16).ToArray();
            byte[] cipherText = encryptedData.Skip(16).ToArray();

            DeriveKeyAndIV(encryptionPassphrase, salt, out byte[] key, out byte[] iv);

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using var ms = new MemoryStream(cipherText);
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);
            string json = reader.ReadToEnd();

            try
            {
                var saveData = System.Text.Json.JsonSerializer.Deserialize<SaveData>(json);
                Line = saveData.Line;
                filePath = saveData.FilePath;
                PlayerInventory.Clear();
                foreach (int id in saveData.InventoryItemIds)
                {
                    var item = InventoryItems.Items.FirstOrDefault(x => x.id == id);
                    if (item != null)
                        PlayerInventory.Add(item);
                }
                NewbottomMid = new Vector2(saveData.BottomMidX, saveData.BottomMidY);
                NewbottomLeft = new Vector2(saveData.BottomLeftX, saveData.BottomLeftY);
                NewbottomRight = new Vector2(saveData.BottomRightX, saveData.BottomRightY);
                ConsoleText = "";
                Progress.ProgressLines(this);
            }
            catch
            {
                ConsoleText = "Error loading save.";
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            Effect invertEffect = Content.Load<Effect>("Invert");
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(backgroundColor);


            if (AreColoursInverted)
            {
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: invertEffect);
            }
            else {
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            }
                
            if (bgImg)
            {
                _spriteBatch.Draw(background, new Vector2(0, 0), null,
      Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            else
            {
                _spriteBatch.Draw(pixel, new Rectangle(0, 0, 1024, 768), backgroundColor);
            }

            _spriteBatch.Draw(Cem3, bottomMid, null,
    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            _spriteBatch.Draw(Cem1, bottomLeft, null,
        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(Cem2, bottomRight, null,
    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            _spriteBatch.Draw(diaBox, new Vector2(0, 550), null,
    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);





            _spriteBatch.DrawString(_font, ReadDialogue(filePath, Line), new Vector2(256, 570), Color.White);

            _spriteBatch.Draw(Portrait, new Vector2(15, 550), null,
Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);






       
            _spriteBatch.DrawString(_font, infoText, new Vector2(150, 150), Color.White);
            _spriteBatch.DrawString(_font, infoText2, new Vector2(150, 100), Color.White);
            if (debugMode)
            {
                string dInfo;
                if (soundEffect != null)
                {
                    dInfo = "Debug Mode" + "\nLine:" + Line.ToString() + "\nCem1:" + Cem1.ToString() + "\nCem2:" + Cem2.ToString() + "\nCem3:" + Cem3.ToString() + "\nBackground:" + background.ToString() + "\nBG:" + bgImg.ToString() + "\nBGColour:" + backgroundColor.ToString() + "\nRoute:" + filePath + "\nSound:" + soundEffect.Name + "\nSave:" + ReadSave(@"Saves/Save.dat") + "\nBCSave:" + ReadSave(@"Saves/BC.dat") + "\nBL:" + bottomLeft.ToString() + "," + NewbottomLeft.ToString() + "\nBM:" + bottomMid.ToString() + "," + NewbottomMid.ToString() + "\nBR:" + bottomRight.ToString() + "," + NewbottomRight.ToString() + "\nBGM:" + BMG.Name;
                }
                else
                {
                    dInfo = "Debug Mode" + "\nLine:" + Line.ToString() + "\nCem1:" + Cem1.ToString() + "\nCem2:" + Cem2.ToString() + "\nCem3:" + Cem3.ToString() + "\nBackground:" + background.ToString() + "\nBG:" + bgImg.ToString() + "\nBGColour:" + backgroundColor.ToString() + "\nRoute:" + filePath + "\nSound: NULL" + "\nSave:" + ReadSave(@"Saves/Save.dat") + "\nBCSave:" + ReadSave(@"Saves/BC.dat") + "\nBL:" + bottomLeft.ToString() + "," + NewbottomLeft.ToString() + "\nBM:" + bottomMid.ToString() + "," + NewbottomMid.ToString() + "\nBR:" + bottomRight.ToString() + "," + NewbottomRight.ToString() + "\nBGM:" + BMG.Name;
                }
                _spriteBatch.DrawString(_font, dInfo, new Vector2(5, 5), Color.Lime);

            }
            if (IsConsoleOpen)
            {
                Color color = new Color();
                if (debugMode)
                {
                    color = Color.Red;
                }
                else
                {
                    color = Color.White;
                }
                _spriteBatch.Draw(pixel, new Rectangle(0, 0, 1024, 768), semiTransparentBlack);
                _spriteBatch.DrawString(_font, "Console", new Vector2(100, 50), color);
                _spriteBatch.DrawString(_font, ConsoleText, new Vector2(100, 100), color);
            }
            _spriteBatch.End();




            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            var screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            var screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

            float scale = Math.Min((float)screenWidth / _internalWidth, (float)screenHeight / _internalHeight);

            int finalWidth = (int)(_internalWidth * scale);
            int finalHeight = (int)(_internalHeight * scale);
            int offsetX = (screenWidth - finalWidth) / 2;
            int offsetY = (screenHeight - finalHeight) / 2;

            Rectangle destinationRect = new Rectangle(offsetX, offsetY, finalWidth, finalHeight);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw((Texture2D)_renderTarget, destinationRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
