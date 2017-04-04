import math


def window_size():
    from ctypes import windll, create_string_buffer

    # stdin handle is -10
    # stdout handle is -11
    # stderr handle is -12

    h = windll.kernel32.GetStdHandle(-12)
    csbi = create_string_buffer(22)
    res = windll.kernel32.GetConsoleScreenBufferInfo(h, csbi)

    if res:
        import struct
        (bufx, bufy, curx, cury, wattr,
         left, top, right, bottom, maxx, maxy) = struct.unpack("hhhhHhhhhhh", csbi.raw)
        sizex = right - left + 1
        sizey = bottom - top + 1
    else:
        sizex, sizey = 80, 25  # can't determine actual size - return default values

    return sizex, sizey


def add_spacing(text, justification="C", width=window_size()[0]):
    if justification == "C":
        spaces = width - len(text) - 3
        return "{0} {1} {2}".format(" " * math.floor(spaces / 2), text, " " * math.ceil(spaces / 2))
    elif justification == "R":
        return " {1} {0}".format(text, " " * (width - len(text) - 2))
    else:
        return " {0} {1}".format(text, " " * (width - len(text) - 2))


def output(text, justification="C", width=None):
    if width is None:
        width = window_size()[0]

    print(add_spacing(text, justification=justification, width=width))
