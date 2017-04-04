import string
import time
import random
import UISystem


def get_randomized(array):
    rand_array = list(array)

    random.shuffle(rand_array)

    return rand_array


def take():
    word = input("The word to hack: ")

    if word == "q":
        return False
    
    cracked = []

    UISystem.output("\n"*100)

    for i in range(0, len(word)):
        cracked.append([" ", False, get_randomized(list(string.printable))])

    hack(word, cracked)

    time.sleep(0.32)
    print()
    time.sleep(0.32)
    UISystem.output(word)
    time.sleep(0.8)
    print()

    return True


def hack(word, cracked):
    for i in range(0, len(string.printable)):
        for j in range(0, len(word)):
            if cracked[j][1]:
                continue

            char = cracked[j][2][i]

            if char in ["\n", "\t"]:
                continue

            if word[j][0] == char:
                cracked[j][0] = char
                cracked[j][1] = True

        output(cracked)
        time.sleep(0.08)


def output(cracked):
    text = ""

    for char in cracked:
        text += char[0]

    UISystem.output(text)


while take():
    continue
