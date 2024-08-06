namespace btnproject
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_start = new Button();
            btn_stop = new Button();
            pb_histo = new PictureBox();
            cmb_obj = new ComboBox();
            tab_things = new TabControl();
            page_box = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            lb_checkcnt = new Label();
            lb_greencnt = new Label();
            lb_redcnt = new Label();
            lb_ngcnt = new Label();
            lb_yellowcnt = new Label();
            lb_ng = new Label();
            lb_yellow = new Label();
            lb_green = new Label();
            lb_red = new Label();
            lb_check = new Label();
            page_milk = new TabPage();
            pnl_cam = new Panel();
            group_info = new GroupBox();
            pb_search = new PictureBox();
            group_trans = new GroupBox();
            btn_logdel = new Button();
            pb_sv = new PictureBox();
            pb_hw = new PictureBox();
            lb_svstatus = new Label();
            lb_hwstatus = new Label();
            tb_server = new TextBox();
            btn_connect = new Button();
            tb_port = new TextBox();
            lb_port = new Label();
            lb_server = new Label();
            cmb_trans = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pb_histo).BeginInit();
            tab_things.SuspendLayout();
            page_box.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            group_info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_search).BeginInit();
            group_trans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_sv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_hw).BeginInit();
            SuspendLayout();
            // 
            // btn_start
            // 
            btn_start.Font = new Font("맑은 고딕", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            btn_start.Location = new Point(616, 234);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(200, 110);
            btn_start.TabIndex = 0;
            btn_start.Text = "START";
            btn_start.UseVisualStyleBackColor = true;
            // 
            // btn_stop
            // 
            btn_stop.Font = new Font("맑은 고딕", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            btn_stop.Location = new Point(616, 350);
            btn_stop.Name = "btn_stop";
            btn_stop.Size = new Size(200, 110);
            btn_stop.TabIndex = 1;
            btn_stop.Text = "STOP";
            btn_stop.UseVisualStyleBackColor = true;
            // 
            // pb_histo
            // 
            pb_histo.Location = new Point(616, 12);
            pb_histo.Name = "pb_histo";
            pb_histo.Size = new Size(200, 200);
            pb_histo.TabIndex = 3;
            pb_histo.TabStop = false;
            // 
            // cmb_obj
            // 
            cmb_obj.DropDownStyle = ComboBoxStyle.Simple;
            cmb_obj.FormattingEnabled = true;
            cmb_obj.Location = new Point(265, 149);
            cmb_obj.Name = "cmb_obj";
            cmb_obj.Size = new Size(726, 333);
            cmb_obj.TabIndex = 4;
            // 
            // tab_things
            // 
            tab_things.Controls.Add(page_box);
            tab_things.Controls.Add(page_milk);
            tab_things.Location = new Point(822, 12);
            tab_things.Name = "tab_things";
            tab_things.SelectedIndex = 0;
            tab_things.Size = new Size(850, 448);
            tab_things.TabIndex = 5;
            // 
            // page_box
            // 
            page_box.Controls.Add(tableLayoutPanel1);
            page_box.Location = new Point(4, 24);
            page_box.Name = "page_box";
            page_box.Padding = new Padding(3);
            page_box.Size = new Size(842, 420);
            page_box.TabIndex = 0;
            page_box.Text = "박스";
            page_box.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(lb_checkcnt, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_greencnt, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_redcnt, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_ngcnt, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_yellowcnt, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_ng, 4, 0);
            tableLayoutPanel1.Controls.Add(lb_yellow, 3, 0);
            tableLayoutPanel1.Controls.Add(lb_green, 2, 0);
            tableLayoutPanel1.Controls.Add(lb_red, 1, 0);
            tableLayoutPanel1.Controls.Add(lb_check, 0, 0);
            tableLayoutPanel1.Location = new Point(6, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 28.5714283F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 71.42857F));
            tableLayoutPanel1.Size = new Size(830, 414);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // lb_checkcnt
            // 
            lb_checkcnt.AutoSize = true;
            lb_checkcnt.BackColor = Color.Transparent;
            lb_checkcnt.BorderStyle = BorderStyle.FixedSingle;
            lb_checkcnt.Dock = DockStyle.Fill;
            lb_checkcnt.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_checkcnt.Location = new Point(3, 118);
            lb_checkcnt.Name = "lb_checkcnt";
            lb_checkcnt.Size = new Size(160, 296);
            lb_checkcnt.TabIndex = 9;
            lb_checkcnt.Text = "0";
            lb_checkcnt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_greencnt
            // 
            lb_greencnt.AutoSize = true;
            lb_greencnt.BackColor = Color.Transparent;
            lb_greencnt.BorderStyle = BorderStyle.FixedSingle;
            lb_greencnt.Dock = DockStyle.Fill;
            lb_greencnt.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_greencnt.ForeColor = Color.Green;
            lb_greencnt.Location = new Point(335, 118);
            lb_greencnt.Name = "lb_greencnt";
            lb_greencnt.Size = new Size(160, 296);
            lb_greencnt.TabIndex = 8;
            lb_greencnt.Text = "0";
            lb_greencnt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_redcnt
            // 
            lb_redcnt.AutoSize = true;
            lb_redcnt.BackColor = Color.Transparent;
            lb_redcnt.BorderStyle = BorderStyle.FixedSingle;
            lb_redcnt.Dock = DockStyle.Fill;
            lb_redcnt.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_redcnt.ForeColor = Color.Red;
            lb_redcnt.Location = new Point(169, 118);
            lb_redcnt.Name = "lb_redcnt";
            lb_redcnt.Size = new Size(160, 296);
            lb_redcnt.TabIndex = 7;
            lb_redcnt.Text = "0";
            lb_redcnt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_ngcnt
            // 
            lb_ngcnt.AutoSize = true;
            lb_ngcnt.BackColor = Color.Transparent;
            lb_ngcnt.BorderStyle = BorderStyle.FixedSingle;
            lb_ngcnt.Dock = DockStyle.Fill;
            lb_ngcnt.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_ngcnt.ForeColor = Color.Purple;
            lb_ngcnt.Location = new Point(667, 118);
            lb_ngcnt.Name = "lb_ngcnt";
            lb_ngcnt.Size = new Size(160, 296);
            lb_ngcnt.TabIndex = 6;
            lb_ngcnt.Text = "0";
            lb_ngcnt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_yellowcnt
            // 
            lb_yellowcnt.AutoSize = true;
            lb_yellowcnt.BackColor = Color.Transparent;
            lb_yellowcnt.BorderStyle = BorderStyle.FixedSingle;
            lb_yellowcnt.Dock = DockStyle.Fill;
            lb_yellowcnt.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_yellowcnt.ForeColor = Color.Yellow;
            lb_yellowcnt.Location = new Point(501, 118);
            lb_yellowcnt.Name = "lb_yellowcnt";
            lb_yellowcnt.Size = new Size(160, 296);
            lb_yellowcnt.TabIndex = 5;
            lb_yellowcnt.Text = "0";
            lb_yellowcnt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_ng
            // 
            lb_ng.AutoSize = true;
            lb_ng.BackColor = Color.DarkGray;
            lb_ng.BorderStyle = BorderStyle.FixedSingle;
            lb_ng.Dock = DockStyle.Fill;
            lb_ng.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_ng.ForeColor = Color.Purple;
            lb_ng.Location = new Point(667, 0);
            lb_ng.Name = "lb_ng";
            lb_ng.Size = new Size(160, 118);
            lb_ng.TabIndex = 4;
            lb_ng.Text = "불량";
            lb_ng.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_yellow
            // 
            lb_yellow.AutoSize = true;
            lb_yellow.BackColor = Color.DarkGray;
            lb_yellow.BorderStyle = BorderStyle.FixedSingle;
            lb_yellow.Dock = DockStyle.Fill;
            lb_yellow.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_yellow.ForeColor = Color.Yellow;
            lb_yellow.Location = new Point(501, 0);
            lb_yellow.Name = "lb_yellow";
            lb_yellow.Size = new Size(160, 118);
            lb_yellow.TabIndex = 3;
            lb_yellow.Text = "노랑";
            lb_yellow.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_green
            // 
            lb_green.AutoSize = true;
            lb_green.BackColor = Color.DarkGray;
            lb_green.BorderStyle = BorderStyle.FixedSingle;
            lb_green.Dock = DockStyle.Fill;
            lb_green.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_green.ForeColor = Color.Green;
            lb_green.Location = new Point(335, 0);
            lb_green.Name = "lb_green";
            lb_green.Size = new Size(160, 118);
            lb_green.TabIndex = 2;
            lb_green.Text = "초록";
            lb_green.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_red
            // 
            lb_red.AutoSize = true;
            lb_red.BackColor = Color.DarkGray;
            lb_red.BorderStyle = BorderStyle.FixedSingle;
            lb_red.Dock = DockStyle.Fill;
            lb_red.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_red.ForeColor = Color.Red;
            lb_red.Location = new Point(169, 0);
            lb_red.Name = "lb_red";
            lb_red.Size = new Size(160, 118);
            lb_red.TabIndex = 1;
            lb_red.Text = "빨강";
            lb_red.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_check
            // 
            lb_check.AutoSize = true;
            lb_check.BackColor = Color.DarkGray;
            lb_check.BorderStyle = BorderStyle.FixedSingle;
            lb_check.Dock = DockStyle.Fill;
            lb_check.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_check.Location = new Point(3, 0);
            lb_check.Name = "lb_check";
            lb_check.Size = new Size(160, 118);
            lb_check.TabIndex = 0;
            lb_check.Text = "검사 수";
            lb_check.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // page_milk
            // 
            page_milk.Location = new Point(4, 24);
            page_milk.Name = "page_milk";
            page_milk.Padding = new Padding(3);
            page_milk.Size = new Size(842, 420);
            page_milk.TabIndex = 1;
            page_milk.Text = "우유";
            page_milk.UseVisualStyleBackColor = true;
            // 
            // pnl_cam
            // 
            pnl_cam.Location = new Point(10, 10);
            pnl_cam.Name = "pnl_cam";
            pnl_cam.Size = new Size(600, 450);
            pnl_cam.TabIndex = 6;
            // 
            // group_info
            // 
            group_info.Controls.Add(cmb_obj);
            group_info.Controls.Add(pb_search);
            group_info.Location = new Point(10, 466);
            group_info.Name = "group_info";
            group_info.Size = new Size(997, 483);
            group_info.TabIndex = 7;
            group_info.TabStop = false;
            // 
            // pb_search
            // 
            pb_search.Location = new Point(6, 15);
            pb_search.Name = "pb_search";
            pb_search.Size = new Size(250, 250);
            pb_search.TabIndex = 4;
            pb_search.TabStop = false;
            // 
            // group_trans
            // 
            group_trans.Controls.Add(btn_logdel);
            group_trans.Controls.Add(pb_sv);
            group_trans.Controls.Add(pb_hw);
            group_trans.Controls.Add(lb_svstatus);
            group_trans.Controls.Add(lb_hwstatus);
            group_trans.Controls.Add(tb_server);
            group_trans.Controls.Add(btn_connect);
            group_trans.Controls.Add(tb_port);
            group_trans.Controls.Add(lb_port);
            group_trans.Controls.Add(lb_server);
            group_trans.Controls.Add(cmb_trans);
            group_trans.Location = new Point(1013, 466);
            group_trans.Name = "group_trans";
            group_trans.Size = new Size(659, 483);
            group_trans.TabIndex = 0;
            group_trans.TabStop = false;
            // 
            // btn_logdel
            // 
            btn_logdel.Location = new Point(569, 19);
            btn_logdel.Name = "btn_logdel";
            btn_logdel.Size = new Size(84, 25);
            btn_logdel.TabIndex = 14;
            btn_logdel.Text = "로그 지우기";
            btn_logdel.UseVisualStyleBackColor = true;
            // 
            // pb_sv
            // 
            pb_sv.Location = new Point(433, 53);
            pb_sv.Name = "pb_sv";
            pb_sv.Size = new Size(30, 30);
            pb_sv.TabIndex = 13;
            pb_sv.TabStop = false;
            // 
            // pb_hw
            // 
            pb_hw.Location = new Point(127, 53);
            pb_hw.Name = "pb_hw";
            pb_hw.Size = new Size(30, 30);
            pb_hw.TabIndex = 0;
            pb_hw.TabStop = false;
            // 
            // lb_svstatus
            // 
            lb_svstatus.AutoSize = true;
            lb_svstatus.Location = new Point(340, 60);
            lb_svstatus.Name = "lb_svstatus";
            lb_svstatus.Size = new Size(87, 15);
            lb_svstatus.TabIndex = 12;
            lb_svstatus.Text = "서버 연결 상태";
            // 
            // lb_hwstatus
            // 
            lb_hwstatus.AutoSize = true;
            lb_hwstatus.Location = new Point(10, 60);
            lb_hwstatus.Name = "lb_hwstatus";
            lb_hwstatus.Size = new Size(111, 15);
            lb_hwstatus.TabIndex = 11;
            lb_hwstatus.Text = "하드웨어 연결 상태";
            // 
            // tb_server
            // 
            tb_server.Location = new Point(61, 19);
            tb_server.Name = "tb_server";
            tb_server.Size = new Size(168, 23);
            tb_server.TabIndex = 10;
            // 
            // btn_connect
            // 
            btn_connect.Location = new Point(486, 19);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(75, 25);
            btn_connect.TabIndex = 0;
            btn_connect.Text = "연 결";
            btn_connect.UseVisualStyleBackColor = true;
            // 
            // tb_port
            // 
            tb_port.Location = new Point(382, 19);
            tb_port.Name = "tb_port";
            tb_port.Size = new Size(81, 23);
            tb_port.TabIndex = 9;
            // 
            // lb_port
            // 
            lb_port.AutoSize = true;
            lb_port.Location = new Point(340, 22);
            lb_port.Name = "lb_port";
            lb_port.Size = new Size(36, 15);
            lb_port.TabIndex = 7;
            lb_port.Text = "PORT";
            // 
            // lb_server
            // 
            lb_server.AutoSize = true;
            lb_server.Location = new Point(10, 22);
            lb_server.Name = "lb_server";
            lb_server.Size = new Size(45, 15);
            lb_server.TabIndex = 6;
            lb_server.Text = "서버 IP";
            // 
            // cmb_trans
            // 
            cmb_trans.DropDownStyle = ComboBoxStyle.Simple;
            cmb_trans.FormattingEnabled = true;
            cmb_trans.Location = new Point(10, 88);
            cmb_trans.Name = "cmb_trans";
            cmb_trans.Size = new Size(643, 389);
            cmb_trans.TabIndex = 5;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1684, 961);
            Controls.Add(group_trans);
            Controls.Add(group_info);
            Controls.Add(pnl_cam);
            Controls.Add(tab_things);
            Controls.Add(pb_histo);
            Controls.Add(btn_stop);
            Controls.Add(btn_start);
            Name = "Main";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pb_histo).EndInit();
            tab_things.ResumeLayout(false);
            page_box.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            group_info.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb_search).EndInit();
            group_trans.ResumeLayout(false);
            group_trans.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_sv).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_hw).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_start;
        private Button btn_stop;
        private PictureBox pb_histo;
        private ComboBox cmb_obj;
        private TabControl tab_things;
        private TabPage page_box;
        private TabPage page_milk;
        private Panel pnl_cam;
        private GroupBox group_info;
        private GroupBox group_trans;
        private PictureBox pb_search;
        private Label lb_port;
        private Label lb_server;
        private ComboBox cmb_trans;
        private Label lb_hwstatus;
        private TextBox tb_server;
        private Button btn_connect;
        private TextBox tb_port;
        private Button btn_logdel;
        private PictureBox pb_sv;
        private PictureBox pb_hw;
        private Label lb_svstatus;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lb_yellowcnt;
        private Label lb_ng;
        private Label lb_yellow;
        private Label lb_green;
        private Label lb_red;
        private Label lb_check;
        private Label lb_checkcnt;
        private Label lb_greencnt;
        private Label lb_redcnt;
        private Label lb_ngcnt;
    }
}
