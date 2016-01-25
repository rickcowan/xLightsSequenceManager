using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace xLightsSequenceManager
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        txtSequenceFile.Text = openFileDialog1.FileName;
                    }
                    catch (Exception ex)
                    {
                        string errorMessage = string.Format(Resources.Resource.ErrorSelectingFile, ex.Message);

                        txtStatus.AppendText(errorMessage);
                        txtStatus.AppendText(Environment.NewLine);

                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            List<XDocument> sequenceDocs = SplitSequence();

            if (sequenceDocs != null && sequenceDocs.Count > 0)
            {
                if (MessageBox.Show(Resources.Resource.OverwriteFile, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    SaveSequenceFiles(sequenceDocs);

                    string successMessage = string.Format(Resources.Resource.Success, sequenceDocs.Count);

                    txtStatus.AppendText(successMessage);
                    txtStatus.AppendText(Environment.NewLine);

                    MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool ValidateData()
        {
            // Validate that a file name has been provided
            if (string.IsNullOrWhiteSpace(txtSequenceFile.Text))
            {
                string errorMessage = Resources.Resource.SequenceFileNotProvided;

                txtStatus.AppendText(errorMessage);
                txtStatus.AppendText(Environment.NewLine);

                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtSequenceFile.Focus();

                return false;
            }
            else
            {
                // Validate that the file exists
                if (!File.Exists(txtSequenceFile.Text))
                {
                    string errorMessage = string.Format(Resources.Resource.FileNotFound, txtSequenceFile.Text);

                    txtStatus.AppendText(errorMessage);
                    txtStatus.AppendText(Environment.NewLine);

                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtSequenceFile.Focus();

                    return false;
                }
            }

            return true;
        }

        private List<XDocument> SplitSequence()
        {
            List<XDocument> retVal = new List<XDocument>();

            try
            {
                XDocument originalSequenceDoc = XDocument.Load(txtSequenceFile.Text, LoadOptions.PreserveWhitespace);

                txtStatus.AppendText("Sequence \"" + txtSequenceFile.Text + "\" loaded.");
                txtStatus.AppendText(Environment.NewLine);

                // Get timing track <Element>
                List<XElement> timingBlocks = originalSequenceDoc.Root.Element("ElementEffects").Elements("Element")
                    .Where(el => el.Attribute("name").Value.ToLower() == cmbTimingTrack.Text)
                    .SingleOrDefault()
                    .Element("EffectLayer").Elements("Effect")
                    .ToList();

                foreach (XElement timingBlock in timingBlocks)
                {
                    int startTime = int.Parse(timingBlock.Attribute("startTime").Value);
                    int endTime = int.Parse(timingBlock.Attribute("endTime").Value);

                    // Create XDocument and copy root
                    XDocument newSequenceDoc = new XDocument(new XElement(originalSequenceDoc.Root.Name));
                    newSequenceDoc.Root.Add(originalSequenceDoc.Root.Attributes().ToArray());

                    // Add all elements except for <ElementEffects>
                    newSequenceDoc.Root.Add(originalSequenceDoc.Root.Elements().Where(el => el.Name != "ElementEffects"));

                    XElement originalElementEffects = originalSequenceDoc.Root.Element("ElementEffects");

                    // Create new <ElementEffects> element
                    XElement newElementEffects = new XElement("ElementEffects");
                    newElementEffects.Add(originalElementEffects.Attributes().ToArray());

                    // Add any element that isn't <Element> to elementEffects
                    newElementEffects.Add(originalElementEffects.Elements().Where(el => el.Name != "Element"));

                    // Iterate through the <Element>s and add to elementEffects
                    foreach (XElement originalElement in originalElementEffects.Elements("Element").Where(el => el.Attribute("name").Value.ToLower() != cmbTimingTrack.Text)) 
                    {
                        XElement newElement = new XElement("Element");
                        newElement.Add(originalElement.Attributes().ToArray());

                        // Add any element that isn't <EffectLayer> to newElement
                        newElement.Add(originalElement.Elements().Where(el => el.Name != "EffectLayer"));

                        // Iterate through <EffectLayer>s and add to newElement
                        foreach (XElement originalEffectLayer in originalElement.Elements("EffectLayer"))
                        {
                            XElement newEffectLayer = new XElement("EffectLayer");
                            newEffectLayer.Add(originalEffectLayer.Attributes().ToArray());

                            // Add any element that isn't <Effect> to newEffectLayer
                            newEffectLayer.Add(originalEffectLayer.Elements().Where(el => el.Name != "Effect"));

                            //Iterate through <Effect>s and add to newEffectLayer
                            int effectID = 0;
                            foreach (XElement originalEffect in originalEffectLayer.Elements("Effect"))
                            {
                                int originalStartTime = int.Parse(originalEffect.Attribute("startTime").Value);
                                int originalEndTime = int.Parse(originalEffect.Attribute("endTime").Value);

                                // This caters to the situation when you have an effect that starts before the current start time but whose end time is within the range of the current timing block
                                bool carryOver = false;
                                if (originalEndTime > startTime && originalStartTime < startTime)
                                {
                                    carryOver = true;
                                }

                                // This caters to the situiation when you have an effect that goes beyond the end time of the current timing block
                                bool overlap = false;
                                if (originalEndTime > endTime)
                                {
                                    overlap = true;
                                }

                                if (carryOver || (originalStartTime >= startTime && originalStartTime < endTime))
                                {
                                    XElement newEffect = new XElement(originalEffect);
                                    newEffect.Attribute("startTime").Value = carryOver ? "0" : (originalStartTime - startTime).ToString();
                                    newEffect.Attribute("endTime").Value = overlap ? (endTime - startTime).ToString() : (originalEndTime - startTime).ToString();

                                    if (newElement.Attribute("type").Value == "model")
                                    {
                                        // Model effects have an id value. If this value is 0 then the value is omitted in the attributes.
                                        if (effectID == 0)
                                        {
                                            if (newEffect.Attribute("id") != null)
                                                newEffect.Attribute("id").Remove();
                                        }
                                        else
                                        {
                                            newEffect.Attribute("id").Value = effectID.ToString();
                                        }
                                    }

                                    // Add the <Effect> to <EffectLayer>
                                    newEffectLayer.Add(newEffect);

                                    effectID++;
                                }
                            }

                            // Add the <EffectLayer> to <Element>
                            newElement.Add(newEffectLayer);
                        }

                        // Add the <Element> to <ElementEffects>
                        newElementEffects.Add(newElement);
                    }

                    // Add the <ElementEffects> to the document
                    newSequenceDoc.Root.Add(newElementEffects);

                    // Add the document to the retVal
                    retVal.Add(newSequenceDoc);
                }
            }
            catch (Exception ex)
            {
                txtStatus.AppendText("ERROR: " + ex.Message);
                txtStatus.AppendText(Environment.NewLine);
            }

            return retVal;
        }

        private void SaveSequenceFiles(List<XDocument> sequenceDocs)
        {
            try
            {
                string filePath = Path.GetDirectoryName(txtSequenceFile.Text);
                int index = 1;

                foreach (XDocument sequenceDoc in sequenceDocs)
                {
                    string fileName = Path.GetFileNameWithoutExtension(txtSequenceFile.Text) + "_" + index.ToString("D2") + ".xml";
                    string fullPath = Path.Combine(filePath, fileName);

                    if (chkUpdateMedia.Checked)
                    {
                        string mediaFile = sequenceDoc.Root.Element("head").Element("mediaFile").Value;
                        string fileNameNoExtension = Path.GetFileNameWithoutExtension(mediaFile);
                        string folderPath = Path.GetFileNameWithoutExtension(mediaFile);
                        sequenceDoc.Root.Element("head").Element("mediaFile").Value = Path.Combine(folderPath, fileNameNoExtension + "_" + index.ToString("D2") + ".mp3");
                    }


                    sequenceDoc.Save(fullPath);

                    txtStatus.AppendText("New sequence \"" + fileName + "\" created.");
                    txtStatus.AppendText(Environment.NewLine);

                    index++;
                }
            }
            catch (Exception ex)
            {
                txtStatus.AppendText("ERROR:" + ex.Message);
                txtStatus.AppendText(Environment.NewLine);
            }
        }

        private void cmbTimingTrack_DropDown(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                XDocument originalSequenceDoc = XDocument.Load(txtSequenceFile.Text, LoadOptions.PreserveWhitespace);

                string currentSelectedText = cmbTimingTrack.Text;

                cmbTimingTrack.Items.Clear();
                cmbTimingTrack.Items.AddRange(originalSequenceDoc.Descendants("ElementEffects").Elements("Element")
                    .Where(el => el.Attribute("type").Value == "timing")
                    .Attributes("name")
                    .Select(at => at.Value)
                    .ToArray());

                if (cmbTimingTrack.Items.Contains(currentSelectedText))
                {
                    cmbTimingTrack.SelectedIndex = cmbTimingTrack.Items.IndexOf(currentSelectedText);
                }
            }
        }
    }
}
