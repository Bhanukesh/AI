'use client'

import Link from 'next/link'
import { usePathname } from 'next/navigation'

export default function AdminLayout({ children }: { children: React.ReactNode }) {
    const pathname = usePathname()

    return (
        <div className="min-h-screen bg-gray-50">
            <nav className="bg-indigo-900 text-white px-6 py-4 flex items-center gap-8">
                <span className="font-bold text-lg">🤖 LLM Council Admin</span>
                <div className="flex gap-6 text-sm">
                    <Link
                        href="/admin/users"
                        className={`hover:text-indigo-200 transition-colors ${pathname === '/admin/users' ? 'text-white font-semibold border-b-2 border-indigo-300 pb-0.5' : 'text-indigo-300'}`}
                    >
                        Users
                    </Link>
                    <Link
                        href="/admin/logs"
                        className={`hover:text-indigo-200 transition-colors ${pathname === '/admin/logs' ? 'text-white font-semibold border-b-2 border-indigo-300 pb-0.5' : 'text-indigo-300'}`}
                    >
                        Delivery Logs
                    </Link>
                </div>
            </nav>
            <main className="container mx-auto p-6">{children}</main>
        </div>
    )
}
