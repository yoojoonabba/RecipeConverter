namespace rcpChange
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btStart = new System.Windows.Forms.Button();
            this.lbState = new System.Windows.Forms.Label();
            this.btSelect = new System.Windows.Forms.Button();
            this.lbPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(513, 53);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(122, 57);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "변환 시작";
            this.btStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // lbState
            // 
            this.lbState.AutoSize = true;
            this.lbState.Location = new System.Drawing.Point(47, 97);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(65, 12);
            this.lbState.TabIndex = 1;
            this.lbState.Text = "상대 : 대기";
            // 
            // btSelect
            // 
            this.btSelect.Location = new System.Drawing.Point(513, 133);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(122, 57);
            this.btSelect.TabIndex = 2;
            this.btSelect.Text = "DB 폴더 선택";
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // lbPath
            // 
            this.lbPath.AutoSize = true;
            this.lbPath.Location = new System.Drawing.Point(47, 75);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(37, 12);
            this.lbPath.TabIndex = 3;
            this.lbPath.Text = "경로 :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 219);
            this.Controls.Add(this.lbPath);
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.lbState);
            this.Controls.Add(this.btStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.Label lbPath;
    }
}

