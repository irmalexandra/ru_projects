from bucket import *
from hashmap import *


# you can also import more than one file,
# although if one imports the other it should
# be enough to import the top level one
# it is recommended to implement different
# classes in different files

def test_map(m):
    try:
        m.insert(5, "fimma")
    except ItemExistsException:
        print("Item already exists")
    try:
        m.insert(4, "fjarri")
    except ItemExistsException:
        print("Item already exists")
    try:
        m.insert(2, "tvistur")
    except ItemExistsException:
        print("Item already exists")
    try:
        m.insert(5, "fimmarimma")
    except ItemExistsException:
        print("Item already exists")
    m[1] = "ás"

    try:
        m.update(4, "fjarkalarki")
    except NotFoundException:
        print("Item not found")
    try:
        m.update(6, "sexxxxxa")
    except NotFoundException:
        print("Item not found")

    m[6] = "sexa"

    print("size of map: " + str(len(m)))
    print(m.contains(12))
    m[12] = "drottning"
    print(m.contains(12))

    print("size of map: " + str(len(m)))
    try:
        print(m.find(4))
    except NotFoundException:
        print("Item not found")
    try:
        print(m[2])
    except NotFoundException:
        print("Item not found")
    try:
        print(m[1])
    except NotFoundException:
        print("Item not found")
    try:
        print(m[5])
    except NotFoundException:
        print("Item not found")
    try:
        print(m.find(6))
    except NotFoundException:
        print("Item not found")
    try:
        print(m[7])
    except NotFoundException:
        print("Item not found")

    print("size of map: " + str(len(m)))
    try:
        m.remove(5)
        print("Item removed")
    except NotFoundException:
        print("Item not found")
    try:
        print(m.find(5))
    except NotFoundException:
        print("Item not found")

    print("size of map: " + str(len(m)))


if __name__ == "__main__":
    print("\nTESTING BUCKET")
    m = Bucket()
    test_map(m)
    print("\nTESTING HASHMAP")
    m = HashMap()
    test_map(m)
