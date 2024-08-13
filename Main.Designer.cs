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
            tab_things = new TabControl();
            page_box = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            lb_ngcnt_b = new Label();
            lb_redcnt = new Label();
            lb_checkcnt_b = new Label();
            lb_yellowcnt = new Label();
            lb_greencnt = new Label();
            lb_ng_b = new Label();
            lb_yellow = new Label();
            lb_green = new Label();
            lb_red = new Label();
            lb_check_b = new Label();
            page_milk = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            lb_checkcnt_m = new Label();
            lb_ngcnt_m = new Label();
            lb_okcnt = new Label();
            lb_ng_m = new Label();
            lb_ok = new Label();
            lb_check_m = new Label();
            group_info = new GroupBox();
            dataGridView1 = new DataGridView();
            dgv_id = new DataGridViewTextBoxColumn();
            dgv_url = new DataGridViewLinkColumn();
            dgv_color = new DataGridViewTextBoxColumn();
            dgv_status = new DataGridViewTextBoxColumn();
            dgv_date = new DataGridViewTextBoxColumn();
            btn_load = new Button();
            btn_del = new Button();
            tb_date = new TextBox();
            tb_status = new TextBox();
            tb_cou = new TextBox();
            tb_url = new TextBox();
            tb_id = new TextBox();
            lb_status = new Label();
            lb_cou = new Label();
            lb_url = new Label();
            lb_date = new Label();
            lb_id = new Label();
            pb_search = new PictureBox();
            group_trans = new GroupBox();
            dgv_trans = new DataGridView();
            trans_num = new DataGridViewTextBoxColumn();
            trans_trans = new DataGridViewTextBoxColumn();
            trans_log = new DataGridViewTextBoxColumn();
            trans_time = new DataGridViewTextBoxColumn();
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
            pb_cam = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pb_histo).BeginInit();
            tab_things.SuspendLayout();
            page_box.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            page_milk.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            group_info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_search).BeginInit();
            group_trans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_trans).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_sv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_hw).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_cam).BeginInit();
            SuspendLayout();
            // 
            // btn_start
            // 
            btn_start.Font = new Font("맑은 고딕", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            btn_start.Location = new Point(656, 226);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(200, 70);
            btn_start.TabIndex = 0;
            btn_start.Text = "START";
            btn_start.UseVisualStyleBackColor = true;
            btn_start.Click += btn_start_Click;
            // 
            // btn_stop
            // 
            btn_stop.Font = new Font("맑은 고딕", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            btn_stop.Location = new Point(656, 302);
            btn_stop.Name = "btn_stop";
            btn_stop.Size = new Size(200, 70);
            btn_stop.TabIndex = 1;
            btn_stop.Text = "STOP";
            btn_stop.UseVisualStyleBackColor = true;
            btn_stop.Click += btn_stop_Click;
            // 
            // pb_histo
            // 
            pb_histo.Location = new Point(656, 10);
            pb_histo.Name = "pb_histo";
            pb_histo.Size = new Size(200, 200);
            pb_histo.TabIndex = 3;
            pb_histo.TabStop = false;
            // 
            // tab_things
            // 
            tab_things.Controls.Add(page_box);
            tab_things.Controls.Add(page_milk);
            tab_things.Location = new Point(862, 12);
            tab_things.Name = "tab_things";
            tab_things.SelectedIndex = 0;
            tab_things.Size = new Size(810, 360);
            tab_things.TabIndex = 5;
            // 
            // page_box
            // 
            page_box.Controls.Add(tableLayoutPanel1);
            page_box.Location = new Point(4, 24);
            page_box.Name = "page_box";
            page_box.Padding = new Padding(3);
            page_box.Size = new Size(802, 332);
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
            tableLayoutPanel1.Controls.Add(lb_ngcnt_b, 4, 1);
            tableLayoutPanel1.Controls.Add(lb_redcnt, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_checkcnt_b, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_yellowcnt, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_greencnt, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_ng_b, 4, 0);
            tableLayoutPanel1.Controls.Add(lb_yellow, 3, 0);
            tableLayoutPanel1.Controls.Add(lb_green, 2, 0);
            tableLayoutPanel1.Controls.Add(lb_red, 1, 0);
            tableLayoutPanel1.Controls.Add(lb_check_b, 0, 0);
            tableLayoutPanel1.Location = new Point(-4, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 28.5714283F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 71.42857F));
            tableLayoutPanel1.Size = new Size(806, 334);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // lb_ngcnt_b
            // 
            lb_ngcnt_b.AutoSize = true;
            lb_ngcnt_b.BackColor = Color.Transparent;
            lb_ngcnt_b.BorderStyle = BorderStyle.FixedSingle;
            lb_ngcnt_b.Dock = DockStyle.Fill;
            lb_ngcnt_b.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_ngcnt_b.ForeColor = Color.Purple;
            lb_ngcnt_b.Location = new Point(647, 95);
            lb_ngcnt_b.Name = "lb_ngcnt_b";
            lb_ngcnt_b.Size = new Size(156, 239);
            lb_ngcnt_b.TabIndex = 10;
            lb_ngcnt_b.Text = "0";
            lb_ngcnt_b.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_redcnt
            // 
            lb_redcnt.AutoSize = true;
            lb_redcnt.BackColor = Color.Transparent;
            lb_redcnt.BorderStyle = BorderStyle.FixedSingle;
            lb_redcnt.Dock = DockStyle.Fill;
            lb_redcnt.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_redcnt.ForeColor = Color.Red;
            lb_redcnt.Location = new Point(164, 95);
            lb_redcnt.Name = "lb_redcnt";
            lb_redcnt.Size = new Size(155, 239);
            lb_redcnt.TabIndex = 9;
            lb_redcnt.Text = "0";
            lb_redcnt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_checkcnt_b
            // 
            lb_checkcnt_b.AutoSize = true;
            lb_checkcnt_b.BackColor = Color.Transparent;
            lb_checkcnt_b.BorderStyle = BorderStyle.FixedSingle;
            lb_checkcnt_b.Dock = DockStyle.Fill;
            lb_checkcnt_b.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_checkcnt_b.ForeColor = Color.Black;
            lb_checkcnt_b.Location = new Point(3, 95);
            lb_checkcnt_b.Name = "lb_checkcnt_b";
            lb_checkcnt_b.Size = new Size(155, 239);
            lb_checkcnt_b.TabIndex = 8;
            lb_checkcnt_b.Text = "0";
            lb_checkcnt_b.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_yellowcnt
            // 
            lb_yellowcnt.AutoSize = true;
            lb_yellowcnt.BackColor = Color.Transparent;
            lb_yellowcnt.BorderStyle = BorderStyle.FixedSingle;
            lb_yellowcnt.Dock = DockStyle.Fill;
            lb_yellowcnt.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_yellowcnt.ForeColor = Color.Yellow;
            lb_yellowcnt.Location = new Point(486, 95);
            lb_yellowcnt.Name = "lb_yellowcnt";
            lb_yellowcnt.Size = new Size(155, 239);
            lb_yellowcnt.TabIndex = 7;
            lb_yellowcnt.Text = "0";
            lb_yellowcnt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_greencnt
            // 
            lb_greencnt.AutoSize = true;
            lb_greencnt.BackColor = Color.Transparent;
            lb_greencnt.BorderStyle = BorderStyle.FixedSingle;
            lb_greencnt.Dock = DockStyle.Fill;
            lb_greencnt.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_greencnt.ForeColor = Color.Green;
            lb_greencnt.Location = new Point(325, 95);
            lb_greencnt.Name = "lb_greencnt";
            lb_greencnt.Size = new Size(155, 239);
            lb_greencnt.TabIndex = 5;
            lb_greencnt.Text = "0";
            lb_greencnt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_ng_b
            // 
            lb_ng_b.AutoSize = true;
            lb_ng_b.BackColor = Color.DarkGray;
            lb_ng_b.BorderStyle = BorderStyle.FixedSingle;
            lb_ng_b.Dock = DockStyle.Fill;
            lb_ng_b.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_ng_b.ForeColor = Color.Purple;
            lb_ng_b.Location = new Point(647, 0);
            lb_ng_b.Name = "lb_ng_b";
            lb_ng_b.Size = new Size(156, 95);
            lb_ng_b.TabIndex = 4;
            lb_ng_b.Text = "불량";
            lb_ng_b.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_yellow
            // 
            lb_yellow.AutoSize = true;
            lb_yellow.BackColor = Color.DarkGray;
            lb_yellow.BorderStyle = BorderStyle.FixedSingle;
            lb_yellow.Dock = DockStyle.Fill;
            lb_yellow.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_yellow.ForeColor = Color.Yellow;
            lb_yellow.Location = new Point(486, 0);
            lb_yellow.Name = "lb_yellow";
            lb_yellow.Size = new Size(155, 95);
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
            lb_green.Location = new Point(325, 0);
            lb_green.Name = "lb_green";
            lb_green.Size = new Size(155, 95);
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
            lb_red.Location = new Point(164, 0);
            lb_red.Name = "lb_red";
            lb_red.Size = new Size(155, 95);
            lb_red.TabIndex = 1;
            lb_red.Text = "빨강";
            lb_red.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_check_b
            // 
            lb_check_b.AutoSize = true;
            lb_check_b.BackColor = Color.DarkGray;
            lb_check_b.BorderStyle = BorderStyle.FixedSingle;
            lb_check_b.Dock = DockStyle.Fill;
            lb_check_b.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_check_b.Location = new Point(3, 0);
            lb_check_b.Name = "lb_check_b";
            lb_check_b.Size = new Size(155, 95);
            lb_check_b.TabIndex = 0;
            lb_check_b.Text = "검사 수";
            lb_check_b.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // page_milk
            // 
            page_milk.Controls.Add(tableLayoutPanel2);
            page_milk.Location = new Point(4, 24);
            page_milk.Name = "page_milk";
            page_milk.Padding = new Padding(3);
            page_milk.Size = new Size(802, 332);
            page_milk.TabIndex = 1;
            page_milk.Text = "우유";
            page_milk.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.Controls.Add(lb_checkcnt_m, 0, 1);
            tableLayoutPanel2.Controls.Add(lb_ngcnt_m, 2, 1);
            tableLayoutPanel2.Controls.Add(lb_okcnt, 1, 1);
            tableLayoutPanel2.Controls.Add(lb_ng_m, 2, 0);
            tableLayoutPanel2.Controls.Add(lb_ok, 1, 0);
            tableLayoutPanel2.Controls.Add(lb_check_m, 0, 0);
            tableLayoutPanel2.Location = new Point(-4, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 28.5714283F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 71.42857F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(806, 336);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // lb_checkcnt_m
            // 
            lb_checkcnt_m.AutoSize = true;
            lb_checkcnt_m.BackColor = Color.Transparent;
            lb_checkcnt_m.BorderStyle = BorderStyle.FixedSingle;
            lb_checkcnt_m.Dock = DockStyle.Fill;
            lb_checkcnt_m.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_checkcnt_m.ForeColor = Color.Black;
            lb_checkcnt_m.Location = new Point(3, 96);
            lb_checkcnt_m.Name = "lb_checkcnt_m";
            lb_checkcnt_m.Size = new Size(262, 240);
            lb_checkcnt_m.TabIndex = 15;
            lb_checkcnt_m.Text = "0";
            lb_checkcnt_m.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_ngcnt_m
            // 
            lb_ngcnt_m.AutoSize = true;
            lb_ngcnt_m.BackColor = Color.Transparent;
            lb_ngcnt_m.BorderStyle = BorderStyle.FixedSingle;
            lb_ngcnt_m.Dock = DockStyle.Fill;
            lb_ngcnt_m.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_ngcnt_m.ForeColor = Color.Red;
            lb_ngcnt_m.Location = new Point(539, 96);
            lb_ngcnt_m.Name = "lb_ngcnt_m";
            lb_ngcnt_m.Size = new Size(264, 240);
            lb_ngcnt_m.TabIndex = 14;
            lb_ngcnt_m.Text = "0";
            lb_ngcnt_m.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_okcnt
            // 
            lb_okcnt.AutoSize = true;
            lb_okcnt.BackColor = Color.Transparent;
            lb_okcnt.BorderStyle = BorderStyle.FixedSingle;
            lb_okcnt.Dock = DockStyle.Fill;
            lb_okcnt.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_okcnt.ForeColor = Color.Green;
            lb_okcnt.Location = new Point(271, 96);
            lb_okcnt.Name = "lb_okcnt";
            lb_okcnt.Size = new Size(262, 240);
            lb_okcnt.TabIndex = 13;
            lb_okcnt.Text = "0";
            lb_okcnt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_ng_m
            // 
            lb_ng_m.AutoSize = true;
            lb_ng_m.BackColor = Color.DarkGray;
            lb_ng_m.BorderStyle = BorderStyle.FixedSingle;
            lb_ng_m.Dock = DockStyle.Fill;
            lb_ng_m.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_ng_m.ForeColor = Color.Red;
            lb_ng_m.Location = new Point(539, 0);
            lb_ng_m.Name = "lb_ng_m";
            lb_ng_m.Size = new Size(264, 96);
            lb_ng_m.TabIndex = 12;
            lb_ng_m.Text = "불량 수";
            lb_ng_m.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_ok
            // 
            lb_ok.AutoSize = true;
            lb_ok.BackColor = Color.DarkGray;
            lb_ok.BorderStyle = BorderStyle.FixedSingle;
            lb_ok.Dock = DockStyle.Fill;
            lb_ok.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_ok.ForeColor = Color.Green;
            lb_ok.Location = new Point(271, 0);
            lb_ok.Name = "lb_ok";
            lb_ok.Size = new Size(262, 96);
            lb_ok.TabIndex = 11;
            lb_ok.Text = "양품 수";
            lb_ok.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_check_m
            // 
            lb_check_m.AutoSize = true;
            lb_check_m.BackColor = Color.DarkGray;
            lb_check_m.BorderStyle = BorderStyle.FixedSingle;
            lb_check_m.Dock = DockStyle.Fill;
            lb_check_m.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_check_m.Location = new Point(3, 0);
            lb_check_m.Name = "lb_check_m";
            lb_check_m.Size = new Size(262, 96);
            lb_check_m.TabIndex = 1;
            lb_check_m.Text = "검사 수";
            lb_check_m.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // group_info
            // 
            group_info.Controls.Add(dataGridView1);
            group_info.Controls.Add(btn_load);
            group_info.Controls.Add(btn_del);
            group_info.Controls.Add(tb_date);
            group_info.Controls.Add(tb_status);
            group_info.Controls.Add(tb_cou);
            group_info.Controls.Add(tb_url);
            group_info.Controls.Add(tb_id);
            group_info.Controls.Add(lb_status);
            group_info.Controls.Add(lb_cou);
            group_info.Controls.Add(lb_url);
            group_info.Controls.Add(lb_date);
            group_info.Controls.Add(lb_id);
            group_info.Controls.Add(pb_search);
            group_info.Location = new Point(10, 374);
            group_info.Name = "group_info";
            group_info.Size = new Size(997, 575);
            group_info.TabIndex = 7;
            group_info.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dgv_id, dgv_url, dgv_color, dgv_status, dgv_date });
            dataGridView1.GridColor = Color.White;
            dataGridView1.Location = new Point(0, 259);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(985, 316);
            dataGridView1.TabIndex = 25;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // dgv_id
            // 
            dgv_id.HeaderText = "ID";
            dgv_id.Name = "dgv_id";
            dgv_id.Resizable = DataGridViewTriState.True;
            // 
            // dgv_url
            // 
            dgv_url.HeaderText = "URL";
            dgv_url.Name = "dgv_url";
            dgv_url.Resizable = DataGridViewTriState.True;
            dgv_url.SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_url.Width = 300;
            // 
            // dgv_color
            // 
            dgv_color.HeaderText = "Color";
            dgv_color.Name = "dgv_color";
            // 
            // dgv_status
            // 
            dgv_status.HeaderText = "Status";
            dgv_status.Name = "dgv_status";
            // 
            // dgv_date
            // 
            dgv_date.HeaderText = "Date";
            dgv_date.Name = "dgv_date";
            dgv_date.Width = 400;
            // 
            // btn_load
            // 
            btn_load.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn_load.Location = new Point(772, 149);
            btn_load.Name = "btn_load";
            btn_load.Size = new Size(110, 50);
            btn_load.TabIndex = 17;
            btn_load.Text = "DOWNLOAD";
            btn_load.UseVisualStyleBackColor = true;
            btn_load.Click += btn_load_Click;
            // 
            // btn_del
            // 
            btn_del.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn_del.Location = new Point(772, 64);
            btn_del.Name = "btn_del";
            btn_del.Size = new Size(110, 50);
            btn_del.TabIndex = 16;
            btn_del.Text = "DELETE";
            btn_del.UseVisualStyleBackColor = true;
            btn_del.Click += btn_del_Click;
            // 
            // tb_date
            // 
            tb_date.Location = new Point(265, 224);
            tb_date.Name = "tb_date";
            tb_date.Size = new Size(375, 23);
            tb_date.TabIndex = 15;
            // 
            // tb_status
            // 
            tb_status.Location = new Point(265, 176);
            tb_status.Name = "tb_status";
            tb_status.Size = new Size(375, 23);
            tb_status.TabIndex = 14;
            // 
            // tb_cou
            // 
            tb_cou.Location = new Point(265, 129);
            tb_cou.Name = "tb_cou";
            tb_cou.Size = new Size(375, 23);
            tb_cou.TabIndex = 13;
            // 
            // tb_url
            // 
            tb_url.Location = new Point(265, 82);
            tb_url.Name = "tb_url";
            tb_url.Size = new Size(375, 23);
            tb_url.TabIndex = 12;
            // 
            // tb_id
            // 
            tb_id.Location = new Point(265, 33);
            tb_id.Name = "tb_id";
            tb_id.Size = new Size(375, 23);
            tb_id.TabIndex = 11;
            // 
            // lb_status
            // 
            lb_status.AutoSize = true;
            lb_status.Location = new Point(265, 158);
            lb_status.Name = "lb_status";
            lb_status.Size = new Size(49, 15);
            lb_status.TabIndex = 9;
            lb_status.Text = "STATUS";
            // 
            // lb_cou
            // 
            lb_cou.AutoSize = true;
            lb_cou.Location = new Point(265, 111);
            lb_cou.Name = "lb_cou";
            lb_cou.Size = new Size(46, 15);
            lb_cou.TabIndex = 8;
            lb_cou.Text = "COLOR";
            // 
            // lb_url
            // 
            lb_url.AutoSize = true;
            lb_url.Location = new Point(265, 64);
            lb_url.Name = "lb_url";
            lb_url.Size = new Size(28, 15);
            lb_url.TabIndex = 7;
            lb_url.Text = "URL";
            // 
            // lb_date
            // 
            lb_date.AutoSize = true;
            lb_date.Location = new Point(265, 206);
            lb_date.Name = "lb_date";
            lb_date.Size = new Size(36, 15);
            lb_date.TabIndex = 6;
            lb_date.Text = "DATE";
            // 
            // lb_id
            // 
            lb_id.AutoSize = true;
            lb_id.Location = new Point(265, 15);
            lb_id.Name = "lb_id";
            lb_id.Size = new Size(19, 15);
            lb_id.TabIndex = 5;
            lb_id.Text = "ID";
            // 
            // pb_search
            // 
            pb_search.Location = new Point(6, 15);
            pb_search.Name = "pb_search";
            pb_search.Size = new Size(250, 230);
            pb_search.TabIndex = 4;
            pb_search.TabStop = false;
            // 
            // group_trans
            // 
            group_trans.Controls.Add(dgv_trans);
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
            group_trans.Location = new Point(1013, 374);
            group_trans.Name = "group_trans";
            group_trans.Size = new Size(659, 575);
            group_trans.TabIndex = 0;
            group_trans.TabStop = false;
            // 
            // dgv_trans
            // 
            dgv_trans.BackgroundColor = Color.White;
            dgv_trans.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_trans.Columns.AddRange(new DataGridViewColumn[] { trans_num, trans_trans, trans_log, trans_time });
            dgv_trans.GridColor = Color.White;
            dgv_trans.Location = new Point(10, 118);
            dgv_trans.Name = "dgv_trans";
            dgv_trans.RowHeadersVisible = false;
            dgv_trans.RowTemplate.Height = 25;
            dgv_trans.Size = new Size(643, 449);
            dgv_trans.TabIndex = 15;
            // 
            // trans_num
            // 
            trans_num.FillWeight = 179.0494F;
            trans_num.HeaderText = "번호";
            trans_num.Name = "trans_num";
            trans_num.Width = 80;
            // 
            // trans_trans
            // 
            trans_trans.FillWeight = 203.045685F;
            trans_trans.HeaderText = "송수신";
            trans_trans.Name = "trans_trans";
            // 
            // trans_log
            // 
            trans_log.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            trans_log.FillWeight = 8.952467F;
            trans_log.HeaderText = "로그";
            trans_log.Name = "trans_log";
            // 
            // trans_time
            // 
            trans_time.FillWeight = 8.952467F;
            trans_time.HeaderText = "시간";
            trans_time.Name = "trans_time";
            trans_time.Width = 120;
            // 
            // btn_logdel
            // 
            btn_logdel.Location = new Point(507, 19);
            btn_logdel.Name = "btn_logdel";
            btn_logdel.Size = new Size(84, 25);
            btn_logdel.TabIndex = 14;
            btn_logdel.Text = "로그 지우기";
            btn_logdel.UseVisualStyleBackColor = true;
            btn_logdel.Click += btn_logdel_Click;
            // 
            // pb_sv
            // 
            pb_sv.BackColor = Color.Red;
            pb_sv.Location = new Point(371, 53);
            pb_sv.Name = "pb_sv";
            pb_sv.Size = new Size(50, 50);
            pb_sv.TabIndex = 13;
            pb_sv.TabStop = false;
            // 
            // pb_hw
            // 
            pb_hw.BackColor = Color.Red;
            pb_hw.Location = new Point(127, 53);
            pb_hw.Name = "pb_hw";
            pb_hw.Size = new Size(50, 50);
            pb_hw.TabIndex = 0;
            pb_hw.TabStop = false;
            // 
            // lb_svstatus
            // 
            lb_svstatus.AutoSize = true;
            lb_svstatus.Location = new Point(278, 68);
            lb_svstatus.Name = "lb_svstatus";
            lb_svstatus.Size = new Size(87, 15);
            lb_svstatus.TabIndex = 12;
            lb_svstatus.Text = "서버 연결 상태";
            // 
            // lb_hwstatus
            // 
            lb_hwstatus.AutoSize = true;
            lb_hwstatus.Location = new Point(10, 68);
            lb_hwstatus.Name = "lb_hwstatus";
            lb_hwstatus.Size = new Size(111, 15);
            lb_hwstatus.TabIndex = 11;
            lb_hwstatus.Text = "하드웨어 연결 상태";
            // 
            // tb_server
            // 
            tb_server.Location = new Point(61, 19);
            tb_server.Name = "tb_server";
            tb_server.Size = new Size(155, 23);
            tb_server.TabIndex = 10;
            // 
            // btn_connect
            // 
            btn_connect.Location = new Point(424, 19);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(75, 25);
            btn_connect.TabIndex = 0;
            btn_connect.Text = "연 결";
            btn_connect.UseVisualStyleBackColor = true;
            btn_connect.Click += btn_connect_Click;
            // 
            // tb_port
            // 
            tb_port.Location = new Point(320, 19);
            tb_port.Name = "tb_port";
            tb_port.Size = new Size(81, 23);
            tb_port.TabIndex = 9;
            // 
            // lb_port
            // 
            lb_port.AutoSize = true;
            lb_port.Location = new Point(278, 22);
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
            // pb_cam
            // 
            pb_cam.Location = new Point(10, 8);
            pb_cam.Name = "pb_cam";
            pb_cam.Size = new Size(640, 360);
            pb_cam.TabIndex = 0;
            pb_cam.TabStop = false;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1684, 961);
            Controls.Add(pb_cam);
            Controls.Add(group_trans);
            Controls.Add(group_info);
            Controls.Add(tab_things);
            Controls.Add(pb_histo);
            Controls.Add(btn_stop);
            Controls.Add(btn_start);
            Name = "Main";
            Text = "Form1";
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)pb_histo).EndInit();
            tab_things.ResumeLayout(false);
            page_box.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            page_milk.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            group_info.ResumeLayout(false);
            group_info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_search).EndInit();
            group_trans.ResumeLayout(false);
            group_trans.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_trans).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_sv).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_hw).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_cam).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_start;
        private Button btn_stop;
        private PictureBox pb_histo;
        private TabControl tab_things;
        private TabPage page_box;
        private TabPage page_milk;
        private GroupBox group_info;
        private GroupBox group_trans;
        private PictureBox pb_search;
        private Label lb_port;
        private Label lb_server;
        private Label lb_hwstatus;
        private TextBox tb_server;
        private Button btn_connect;
        private TextBox tb_port;
        private Button btn_logdel;
        private PictureBox pb_sv;
        private PictureBox pb_hw;
        private Label lb_svstatus;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lb_greencnt;
        private Label lb_ng_b;
        private Label lb_yellow;
        private Label lb_green;
        private Label lb_red;
        private Label lb_check_b;
        private Label lb_redcnt;
        private Label lb_checkcnt_b;
        private Label lb_yellowcnt;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lb_okcnt;
        private Label lb_ng_m;
        private Label lb_ok;
        private Label lb_check_m;
        private Label lb_ngcnt_m;
        private TextBox tb_date;
        private TextBox tb_status;
        private TextBox tb_cou;
        private TextBox tb_url;
        private TextBox tb_id;
        private Label lb_status;
        private Label lb_cou;
        private Label lb_url;
        private Label lb_date;
        private Label lb_id;
        private Button btn_load;
        private Button btn_del;
        private Label lb_ngcnt_b;
        private Label lb_checkcnt_m;
        private PictureBox pb_cam;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn dgv_id;
        private DataGridViewLinkColumn dgv_url;
        private DataGridViewTextBoxColumn dgv_color;
        private DataGridViewTextBoxColumn dgv_status;
        private DataGridViewTextBoxColumn dgv_date;
        private DataGridView dgv_trans;
        private DataGridViewTextBoxColumn trans_num;
        private DataGridViewTextBoxColumn trans_trans;
        private DataGridViewTextBoxColumn trans_log;
        private DataGridViewTextBoxColumn trans_time;
    }
}
