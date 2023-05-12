import random

with open("list.txt",mode="r",encoding="utf-8") as file:
    dataList = file.read().split("\n")
    print(random.choice(dataList))
    