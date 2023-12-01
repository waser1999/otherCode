from tkinter import *
from tkinter import ttk
import edge_tts as tts
import os

root = Tk()
root.title("edge-tts")

speaker = StringVar()
# 设置默认值
speaker.set("zh-CN-XiaoxiaoNeural")

ttk.Label(text="请输入你要用Edge大声朗读的文本：").pack(anchor="w")
text = Text(root, width=100, height=10)
text.pack(fill="x")

frmChoose = ttk.Frame(root, padding=100).pack()

ttk.Label(frmChoose, text="请选择语音来源：").pack(anchor="w")
ttk.Radiobutton(frmChoose, text="晓晓（女）", variable=speaker, value="zh-CN-XiaoxiaoNeural").pack(anchor="w")
ttk.Radiobutton(frmChoose, text="晓艺（女）", variable=speaker, value="zh-CN-XiaoyiNeural").pack(anchor="w")
ttk.Radiobutton(frmChoose, text="云间（男）", variable=speaker, value="zh-CN-YunjianNeural").pack(anchor="w")
ttk.Radiobutton(frmChoose, text="云曦（男）", variable=speaker, value="zh-CN-YunxiNeural").pack(anchor="w")
ttk.Radiobutton(frmChoose, text="云霞（男）", variable=speaker, value="zh-CN-YunxiaNeural").pack(anchor="w")
ttk.Radiobutton(frmChoose, text="云阳（男）", variable=speaker, value="zh-CN-YunyangNeural").pack(anchor="w")

ttk.Label(frmChoose, text="输出文件名：").pack(anchor="w")
entry = ttk.Entry(frmChoose)
entry.pack(anchor="w")

# Button 方法
def tts():
    textToRead = text.get(1.0, "end").strip()
    speakerChosen = speaker.get()
    name = entry.get().strip() + ".mp3"
    cmd = "python -m edge_tts -v " + speakerChosen + " -t " + textToRead + " --write-media " + name.strip()
    res = os.system(cmd)

ttk.Button(root, text="合成", command=tts).pack()

root.mainloop()