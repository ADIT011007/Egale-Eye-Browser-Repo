using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Phoenix_Browser
{
    public partial class licenses : MaterialForm
    {
        // Data structure to store information about libraries and licenses
        private Dictionary<string, string> libraryInfo = new Dictionary<string, string>();
        readonly MaterialSkin.MaterialSkinManager materialSkinManager;
        public licenses()
        {
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            if (Properties.Settings.Default.settings_dark == true)
            {
                materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            }
            else
            {
                materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;

            }
            materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Indigo700, MaterialSkin.Primary.Blue500, MaterialSkin.Primary.Indigo700, MaterialSkin.Accent.Blue100, MaterialSkin.TextShade.WHITE);
            InitializeComponent();

            // Add the new license information for "Copyright 2023 Adit"
            libraryInfo.Add("Copyright 2023 Adit", @"
Copyright 2023 Adit

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.");

            // Add the license information for Krypton Toolkit
            libraryInfo.Add("Krypton.Toolkit 80.23.11.321", @"
BSD 3-Clause License

Copyright (c) 2017 - 2023, Krypton Suite
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

   Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

   Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

   Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS ""AS IS"" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

Source: https://github.com/Krypton-Suite/Standard-Toolkit");

            // Add the MIT License information for the second library
            libraryInfo.Add("MIT License (Second Library)", @"
MIT License

Copyright (c) <2023> <Adit Gadkary>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice (including the next paragraph) shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

SPDX web page: https://spdx.org/licenses/MIT.html
Notice: This license content is provided by the SPDX project. For more information about licenses.nuget.org, see our documentation.

Data pulled from spdx/license-list-data on February 9, 2023");

            // Add the MIT License information for the third library (SVG.NET)
            libraryInfo.Add("The MIT License (SVG.NET)", @"
The MIT License (MIT)

Copyright (c) [2023] [Adit Gadkary]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Source: https://github.com/svg-net/SVG");

            // Add the license information for CefSharp library
            libraryInfo.Add("CefSharp", @"
Copyright © The CefSharp Authors. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

* Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above
copyright notice, this list of conditions and the following disclaimer
in the documentation and/or other materials provided with the
distribution.

* Neither the name of Google Inc. nor the name Chromium Embedded
Framework nor the name CefSharp nor the names of its contributors
may be used to endorse or promote products derived from this software
without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
""AS IS"" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

Source: https://github.com/cefsharp/cefsharp");

            // Populate the ListBox with library names
            foreach (var library in libraryInfo.Keys)
            {
                listBox1.Items.Add(library);
            }

            // Attach the event handler to the SelectedIndexChanged event of the ListBox
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;

            // Call the DisplayCredits method during initialization
            DisplayCredits();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (listBox1.SelectedIndex != -1)
            {
                // Get the selected library name
                string selectedLibrary = listBox1.SelectedItem.ToString();

                // Get the license for the selected library
                string selectedLicense = libraryInfo[selectedLibrary];

                // Display the library name and license in the multiline TextBox
                richTextBox1.Text = selectedLicense;
            }
        }

        // Method to display credits in a MessageBox
        private void DisplayCredits()
        {
            MessageBox.Show(
                "Icons:\n" +
                "- Front and Forward Icon downloaded from freeicons.io. Icon maker: https://freeicons.io/profile/714\n" +
                "- Reload Icon downloaded from freeicons.io. Icon maker: https://freeicons.io/profile/8908\n" +
                "- X Icon by https://freeicons.io/profile/714\n" +
                "- Settings Icon by https://freeicons.io/profile/3277\n\n" +
                "Special Thanks:\n" +
                "- Name: Adit\n" +
                "- Contribution: Main Developer and UI Designer\n\n" +
                "Thank you to Adit for being the main developer and UI designer, and to all the authors and communities behind these wonderful projects, contributing to the development of Eagle Eye Browser.",
                "Credits and Special Thanks",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void licenses_Load(object sender, EventArgs e)
        {

        }
    }
}
