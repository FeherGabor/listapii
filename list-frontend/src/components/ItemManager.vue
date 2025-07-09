<template>
  <div>
    <h2>Lista</h2>
    <input v-model="newItem" placeholder="Új elem" />
    <button @click="addItem">Hozzáadás</button>

    <ul>
      <li v-for="item in items" :key="item.id">
        <input v-model="item.name" @blur="updateItem(item)" />
        <button @click="deleteItem(item.id)">Törlés</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

const items = ref([])
const newItem = ref('')

const fetchItems = async () => {
  const stored = localStorage.getItem('items')
  if (stored) {
    items.value = JSON.parse(stored)
  } else {
    const res = await axios.get('http://localhost:5184/api/items')
    items.value = res.data
    localStorage.setItem('items', JSON.stringify(items.value))
  }
}

const addItem = async () => {
  if (!newItem.value.trim()) {
    alert("Nem lehet üres elemet hozzáadni.")
    return
  }
  const res = await axios.post('http://localhost:5184/api/items', { name: newItem.value.trim() })
  items.value.push(res.data)
  localStorage.setItem('items', JSON.stringify(items.value))
  newItem.value = ''
}

const updateItem = async (item) => {
  await axios.put(`http://localhost:5184/api/items/${item.id}`, item)
  localStorage.setItem('items', JSON.stringify(items.value))
}

const deleteItem = async (id) => {
  await axios.delete(`http://localhost:5184/api/items/${id}`)
  items.value = items.value.filter(i => i.id !== id)
  localStorage.setItem('items', JSON.stringify(items.value))
}

onMounted(fetchItems)
</script>
