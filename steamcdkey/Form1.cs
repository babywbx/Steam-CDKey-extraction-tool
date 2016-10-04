using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KeyExtractor
{
  public partial class Form1 : Form
  {
    /// <summary>
    /// This sets what present mode it uses.
    /// 0 - classic mode. Keys will be split into new lines
    /// 1 - ASF mode. Keys will be split using comma.
    /// </summary>
    private int m_PresentMode = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Form1"/> class.
    /// </summary>
    public Form1()
    {
      InitializeComponent();
      
      // Update status stip.
      this.toolStripStatusLabel1.Text = "";
      this.statusStrip1.Refresh();

            // Default mode is ASF.
            this.comboBoxMode.SelectedIndex = 1;
    }

    /// <summary>
    /// Extracts the keys from string.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    private List<string> ExtractKeysFromString(string source)
    {
      MatchCollection m = Regex.Matches(source, "([0-9A-Z]{5})(?:\\-[0-9A-Z]{5}){2,3}",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);
      List<string> result = new List<string>();
      if (m.Count > 0)
      {
        foreach (Match v in m)
        {
          result.Add(v.Value);
        }
      }
      return result;
    }

    /// <summary>
    /// Extracts the keys and copy to clipboard.
    /// </summary>
    private void ExtractKeysFromClipBoardAndCopyToClipboard()
    {
      string plainText;
      plainText = Clipboard.GetText();

      List<string> listStrKeys = ExtractKeysFromString(plainText);
      if (listStrKeys.Count > 0)
      {
        string strKeys;

        switch (m_PresentMode)
        {
          case 1:
            strKeys = string.Join(",", listStrKeys.ToArray());
            break;
          case 0:
          default:
            strKeys = string.Join(Environment.NewLine, listStrKeys.ToArray());
            break;
        }

        try
        {
          Clipboard.SetText(strKeys);
          richTextBox1.Text = (strKeys);
          UpdateToolStripStatusLabel(string.Format("{0} 个激活码已经复制到您的剪贴板了！", listStrKeys.Count));
          MessageBox.Show(string.Format("{0} 个激活码已经复制到您的剪贴板了！", listStrKeys.Count), "提示");
        }
        catch
        {
          MessageBox.Show(strKeys, "Ctrl+C 复制");
        }
      }
      else
      {
        MessageBox.Show("找不到任何激活码！请重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// Updates the tool strip status label.
    /// </summary>
    /// <param name="text">The text.</param>
    private void UpdateToolStripStatusLabel(string text)
    {
      this.toolStripStatusLabel1.Text = text;
      this.statusStrip1.Refresh();
    }

    /// <summary>
    /// Handles the Click event of the buttonExtractKeys control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void buttonExtractKeys_Click(object sender, EventArgs e)
    {
      ExtractKeysFromClipBoardAndCopyToClipboard();
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the comboBoxMode control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
    {
      m_PresentMode = this.comboBoxMode.SelectedIndex;
      UpdateToolStripStatusLabel(" 您选择了 " + this.comboBoxMode.SelectedItem.ToString() + " 模式");
    }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By wbx https://wbx123.com 于 2016年10月3日 编写", "关于");
            DialogResult dr;
            dr = MessageBox.Show("点击(是)查看更多，点击(否)退出", "更多信息", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                System.Diagnostics.Process.Start("https://wbx123.com/100");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr2;
            dr2 = MessageBox.Show("确认清空剪贴板？", "确认清空剪贴板？", MessageBoxButtons.YesNo);
            if (dr2 == DialogResult.Yes)
                Clipboard.Clear();
                MessageBox.Show("成功！");
        }
    }
}
